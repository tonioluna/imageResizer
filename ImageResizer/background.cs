using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ImageResizer
{
    public partial class mainForm : Form
    {
        #region Interfaces to Main Thread
        private void backgroundProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = null;

            ProfileHandler.Profile conversion_params = null;

            log.debug("Getting conversion params from main thread");

            // Get the parameters from the main thread
            Invoke(new MethodInvoker(delegate
            {
                conversion_params = get_current_params();
            }
            ));

            if (conversion_params == null)
            {
                log.error("Unable to retrieve conversion params, aborting working thread");
                e.Result = this.get_lang_string("dialog_worker_params_retr_err");
                return;
            }

            // Check we have a valid folder
            if (!Directory.Exists(conversion_params.input_folder))
            {
                log.error("Input directory '{0}' does not exist", conversion_params.input_folder);
                MessageBox.Show("Input directory does not exist!", "Incorrect input directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            log.info("Input folder: {0}", conversion_params.input_folder);
            if (Directory.Exists(conversion_params.output_folder))
            {
                log.error("Output directory '{0}' exist.", conversion_params.input_folder);
                MessageBox.Show("Output directory does exists!\r\nPlease select another folder.", "Output directory exists!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            log.info("Output folder: {0}", conversion_params.output_folder);

            // Create output folder if it doesn't exist
            if (Directory.Exists(conversion_params.output_folder))
            {
                log.debug("Output folder exists");
            }
            else
            {
                log.info("Output folder does not exist. Creating it...");
                try
                {
                    Directory.CreateDirectory(conversion_params.output_folder);
                }
                catch (Exception ex)
                {
                    log.error("Unable to create output folder");
                    log.debug(ex.ToString());
                    MessageBox.Show("Output directory could not be created. Path is incorrect!", "Unable to create output folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                log.debug("Output folder succesfully created");
            }

            // Locate the available images
            string[] all_files = Directory.GetFiles(conversion_params.input_folder);

            // Filter files per extension
            List<String> files = new List<string>();
            int file_count = all_files.Length;

            for (int i = 0; i < file_count; i++)
            {
                string file = all_files[i];
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".jpg" || ext == ".jpeg")
                {
                    files.Add(file);
                }
                else
                {
                    log.warning("Unsupported file extension: {0}", ext);
                }
            }

            file_count = files.Count;

            log.debug("Located {0} matching files on input directory", file_count);

            for (int i = 0; i < file_count; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                string file = Path.GetFileName(files[i]);
                string ffile = Path.Combine(conversion_params.input_folder, file);
                string ext = Path.GetExtension(file).ToLower();

                log.debug("Processing {0}", ffile);
                worker.ReportProgress(100 * i / file_count, string.Format("{0}/{1} - {2}", i, file_count, file));

                if (ext == ".jpg" || ext == ".jpeg")
                {
                    resize_image(ffile,
                                 conversion_params.output_folder,
                                 conversion_params.method,
                                 conversion_params.width,
                                 conversion_params.height
                                 );
                }
                else
                {
                    log.warning("Unsupported file extension: {0}", ext);
                }
            }
        }

        private void backgroundProcessor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            currentImgLabel.Text = e.UserState as string;
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundProcessor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            state = WorkState.idle;
            startButton.Text = get_lang_string("gui_dialog_startButton_start");

            if (e.Cancelled)
                currentImgLabel.Text = get_lang_string("dialog_currImgLabel_canceled");
            else
            {
                if (e.Result == null)
                {
                    currentImgLabel.Text = "Operation was succesfully completed";
                }
                else
                {
                    log.error("Got an error from the worker thread: {0}", e.Result as string);
                    MessageBox.Show("Something failed. Work operation returned an error:\r\n" +
                        e.Result as string,
                        "Error Ocurred",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                }
            }
            progressBar.Value = 0;

            if (this.bulk_test_current_index != -1)
            {
                do_bulk_test_work();
            }
            else if (this.outFolAutoSelCB.Checked){
                update_auto_folder_selection();
            }
        }

        #endregion Interfaces to Main Thread

        #region Resize Functions
        void resize_image(string input_path, string output_folder, ResizeMethod.Method method, int box_width, int box_height)
        {

            log.debug("Resizing image");
            log.debug("   Source.......: " + input_path);
            log.debug("   Output folder: " + output_folder);
            log.debug("   Method.......: " + method);
            log.debug("   Box Width....: " + box_width);
            log.debug("   Box Height...: " + box_height);

            Image loaded_image = Image.FromFile(input_path);
            Image output_image;

            int source_width = loaded_image.Width;
            int source_height = loaded_image.Height;
            int source_x = -1;
            int source_y = -1;

            int destination_x = -1;
            int destination_y = -1;
            int destination_width = -1;
            int destination_height = -1;

            source_width = loaded_image.Width;
            source_height = loaded_image.Height;

            if (method == ResizeMethod.Method.stretch)
            {
                source_x = 0;
                source_y = 0;

                destination_x = 0;
                destination_y = 0;
                destination_height = box_height < source_height ? box_height : source_height;
                destination_width = box_width < source_width ? box_width : source_width;
            }
            else if (method == ResizeMethod.Method.fit_on_box)
            {
                double boxRatio = Convert.ToDouble(box_width) / Convert.ToDouble(box_height);
                double sourceRatio = Convert.ToDouble(source_width) / Convert.ToDouble(source_height);

                if (sourceRatio > boxRatio)
                {
                    // original ratio is larger than destination ratio. Thus, image width will match box's width and height will be smaller
                    destination_width = box_width < source_width ? box_width : source_width;
                    destination_height = (int)(destination_width / sourceRatio);
                }
                else
                {
                    destination_height = box_height < source_height ? box_height : source_height;
                    destination_width = (int)(sourceRatio * destination_height);
                }
                source_x = 0;
                source_y = 0;

                destination_x = 0;
                destination_y = 0;
            }
            else if (method == ResizeMethod.Method.cut_excess)
            {
                double boxRatio = Convert.ToDouble(box_width) / Convert.ToDouble(box_height);
                double sourceRatio = Convert.ToDouble(source_width) / Convert.ToDouble(source_height);

                log.debug("Resize intermediate parameters:");
                log.debug("    Box Ratio........: " + boxRatio);
                log.debug("    Source Ratio.....: " + sourceRatio);

                if (source_width < destination_width && source_height < destination_height)
                {
                    log.debug("Source image fits on output box, not cutting any side of the image");

                    source_x = 0;
                    source_y = 0;

                    destination_x = 0;
                    destination_y = 0;
                    destination_height = source_height;
                    destination_width = source_width;
                }
                else if (sourceRatio > boxRatio)
                {
                    // original image is wider than the box target and it's ratio is also wider. 
                    // Then, need to reduce image height in order to
                    // 1) don't stretch image
                    // 2) Meet box size

                    destination_width = box_width;
                    double taken_width = -1.0;

                    if (source_height > box_height)
                    {
                        destination_height = box_height;
                        taken_width = boxRatio * Convert.ToDouble(source_height);
                    }
                    else
                    {
                        destination_height = source_height;
                        taken_width = box_width;
                    }
                    int excess_side = (int)((source_width - taken_width) / 2);

                    log.debug("    Taken width......: " + taken_width);
                    log.debug("    Excess side......: " + excess_side);

                    source_width = (int)taken_width;

                    source_y = 0;
                    source_x = (int)excess_side;

                    destination_x = 0;
                    destination_y = 0;

                }
                else
                {
                    // original image is higher than the box target and it's ratio is also higher. 
                    // Then, need to reduce image height in order to
                    // 1) don't stretch image
                    // 2) Meet box size

                    destination_height = box_height;
                    double taken_height = -1.0;

                    if (source_width > box_width)
                    {
                        destination_width = box_width;
                        taken_height = Convert.ToDouble(source_width) / boxRatio;
                    }
                    else
                    {
                        destination_width = source_width;
                        taken_height = box_height;
                    }
                    int excess_top = (int)((source_height - taken_height) / 2);

                    log.debug("    Taken height.....: " + taken_height);
                    log.debug("    Excess top.......: " + excess_top);

                    source_height = (int)taken_height;

                    source_y = excess_top;
                    source_x = 0;

                    destination_x = 0;
                    destination_y = 0;
                }
            }
            else if (method == ResizeMethod.Method.add_fill)
            {
                double boxRatio = Convert.ToDouble(box_width) / Convert.ToDouble(box_height);
                double sourceRatio = Convert.ToDouble(source_width) / Convert.ToDouble(source_height);

                if (sourceRatio > boxRatio)
                {
                    // original ratio is larger than destination ratio. Thus, image width will match box's width and height will be smaller
                    destination_width = box_width < source_width ? box_width : source_width;
                    destination_height = (int)(destination_width / sourceRatio);
                }
                else
                {
                    destination_height = box_height < source_height ? box_height : source_height;
                    destination_width = (int)(sourceRatio * destination_height);
                }
                source_x = 0;
                source_y = 0;

                destination_x = 0;
                destination_y = 0;
            }
            
            else
            {
                log.error("Unsupported resize method: {0}", method);
                throw new Exception(string.Format("Internal error, unsupported resize method: {0}", method));
                return;
            }

            log.debug("Resize caulculated parameters:");
            log.debug("    Source Width......: " + source_width);
            log.debug("    Source Height.....: " + source_height);
            log.debug("    Source X..........: " + source_x);
            log.debug("    Source Y..........: " + source_y);
            log.debug("    Destination X.....: " + destination_x);
            log.debug("    Destination Y.....: " + destination_y);
            log.debug("    Destination Width.: " + destination_width);
            log.debug("    Destination Height: " + destination_height);

            Bitmap bmPhoto = new Bitmap(destination_width, destination_height,
                         PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(loaded_image.HorizontalResolution,
                                    loaded_image.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;


            grPhoto.DrawImage(loaded_image,
                new Rectangle(destination_x, destination_y, destination_width, destination_height),
                new Rectangle(source_x, source_y, source_width, source_height),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            output_image = bmPhoto;

            //List<ImageCodecInfo> ici = new List<ImageCodecInfo>();
            //ici.AddRange(ImageCodecInfo.GetImageEncoders());

            log.warning("Missing to add encoding compression level into the output format");

            //ici.MimeType 
            output_image.Save(Path.Combine(output_folder, Path.GetFileName(input_path)), ImageFormat.Jpeg);
            loaded_image.Dispose();
            output_image.Dispose();

        }

        #endregion

    }
}
