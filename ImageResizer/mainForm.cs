#define TEST_PARAMS
//#define USE_SPANISH

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using System.Globalization;
using System.Resources;
using System.Diagnostics;
using ImageResizer.Properties;

namespace ImageResizer
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();

            log = new SimpleLogger("image_resizer.log", SimpleLogger.LogLevel.debug);

            GuiInitialization();
        }

        #region Variables
        public SimpleLogger log;
        ProfileHandler profiles;
        WorkState state;
        public ResourceManager resourceManager;    // declare Resource manager to access to specific cultureinfo
        public CultureInfo culture;            // declare culture info
        bool events_enabled = false;
        List<ToolStripMenuItem> lang_tool_strip_items;
        List<TranslatableGuiItem> translatable_gui_items;
        #endregion

        void set_language(string lang)
        {
            if (lang == "Spanish")
            {
                this.culture = CultureInfo.CreateSpecificCulture("es");
            }
            else
            {
                this.culture = CultureInfo.CreateSpecificCulture("en");
            }

            foreach (ToolStripMenuItem tsmi in this.lang_tool_strip_items)
            {
                tsmi.Checked = (tsmi.Tag as string) == lang;
            }
            

            // Language-specific initialization
            // ResourceSet s = resourceManager.GetResourceSet(this.culture, false, false);

            foreach (TranslatableGuiItem tgi in this.translatable_gui_items)
            {
                tgi.reload_text();
            }
            
            // Custom controls
            startButton.Text = get_lang_string("dialog_startButton_start");

        }

        private void GuiInitialization()
        {
            this.resourceManager = new ResourceManager("ImageResizer.Resource.Res", typeof(mainForm).Assembly);

            this.state = WorkState.idle;


            // Initialize translation-related lists
            this.lang_tool_strip_items = new List<ToolStripMenuItem>();
            this.lang_tool_strip_items.Add(this.englishToolStripMenuItem);
            this.lang_tool_strip_items.Add(this.españolToolStripMenuItem);

            this.translatable_gui_items = new List<TranslatableGuiItem>();
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.aboutToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.fileToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_Control(this, this.heigthLabel));
            this.translatable_gui_items.Add(new TGI_Control(this, this.inputFolderGroupBox));
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.languajeToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.exitToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_Control(this, this.methodLabel));
            this.translatable_gui_items.Add(new TGI_Control(this, this.outFolAutoSelCB));
            this.translatable_gui_items.Add(new TGI_Control(this, this.outputFolderGroupBox));
            this.translatable_gui_items.Add(new TGI_Control(this, this.outputParamsBox));
            this.translatable_gui_items.Add(new TGI_Control(this, this.profileLabel));
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.showLoggerToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.toolsToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_Control(this, this.widthLabel));

            ResizeMethods methods = new ResizeMethods();

            // List supported methods
            this.resizeMethodCmBx.Items.Clear();
            foreach (ResizeMethod m in methods.methods)
            {
                this.resizeMethodCmBx.Items.Add(m);
            }

            // List supported profiles
            this.profiles = new ProfileHandler();
            this.profileSelector.Items.Clear();
            this.profileSelector.SelectedIndex = -1;
            foreach (ProfileHandler.Profile p in profiles.get_profiles())
            {
                this.profileSelector.Items.Add(p);
            }
            if (this.profileSelector.Items.Count > 0)
                this.profileSelector.SelectedIndex = 0;

            // Load saved settings
            set_language(Settings.Default.language);
            if (Settings.Default.activeProfile != "")
            {
                log.debug("Loading last used profile: {0}", Settings.Default.activeProfile);
                LoadProfile(this.profiles.get_profile_by_name(Settings.Default.activeProfile));
            }
            else
            {
                log.debug("Loading currently selected profile as no data from last active profile is saved");
                LoadProfile((this.profileSelector.SelectedItem as ProfileHandler.Profile));
            }

#if TEST_PARAMS
            inputFolderTB.Text = @"C:\Users\tonio\Pictures\FotosDeLaVillaForExperiments";
            outputFolderTB.Text = @"C:\Users\tonio\Pictures\FotosDeLaVillaForExperiments\out";
            setResizeMetdhod(ResizeMethod.Method.stretch);
#endif

            events_enabled = true;
        }

        private void setResizeMetdhod(ResizeMethod.Method method)
        {
            int index = -1;

            log.debug("Changing resize method to {0}", method.ToString());

            foreach (object obj in resizeMethodCmBx.Items)
            {
                index += 1;
                ResizeMethod m = (ResizeMethod)obj;
                if (m.method == method) {
                    resizeMethodCmBx.SelectedIndex = index;
                    return;
                }
            }
            throw new Exception("Internal error, method not found on available list on GUI");
        }

        private void LoadProfile(ProfileHandler.Profile profile)
        {
            if (profile == null) {
                log.debug("Skipping profile load, got null pointer");
            }
            log.debug("Loading profile {0}", profile.name);

            setResizeMetdhod(profile.method);
            widthTB.Text = profile.width.ToString();
            heightTB.Text = profile.height.ToString();

            Settings.Default.activeProfile = profile.name;
            Settings.Default.Save();
        }

        private string get_lang_string(string name)
        {
            string strn = this.resourceManager.GetString(name, this.culture);
            this.log.debug("Get language string: '{0}': '{1}'", name, strn);
            return "+" + strn;
        }

        private void inputFolderTB_TextChanged(object sender, EventArgs e)
        {
            string folder = inputFolderTB.Text;
            if (Directory.Exists(folder))
            {
                inputFolderTB.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else 
            {
                inputFolderTB.BackColor = Color.Coral;
            }
        }

        private void profileSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!events_enabled) return;

            this.events_enabled = false;

            if (profileSelector.SelectedIndex >= 0)
            {
                LoadProfile(profileSelector.SelectedItem as ProfileHandler.Profile);
            }

            this.events_enabled = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (state == WorkState.idle)
            {
                do_stuff();
            }
            else if (state == WorkState.running)
            {
                cancel_stuff();
            }
            else if (state == WorkState.canceling)
            {
                log.warning("Cancellation already in progress...");
            }
            else { 
                throw new Exception(string.Format("Unknown work state: {0}", state));
            }
        }

        private void cancel_stuff()
        {
            log.info("Cancelling work");
            backgroundProcessor.CancelAsync();

            startButton.Text = get_lang_string("dialog_startButton_cancelling");
            state = WorkState.canceling;
        }

        private void do_stuff()
        {
            log.debug("Starting operation...");

            progressBar.Maximum = 100;
            progressBar.Minimum = 0;

            startButton.Text = get_lang_string("dialog_startButton_cancel");
            state = WorkState.running;

            backgroundProcessor.RunWorkerAsync();
        }

        void resize_image(string input_path, string output_folder, ResizeMethod.Method method, int destWidth, int destHeight) {
            Image img = Image.FromFile(input_path);
            Image out_img;

            if (method == ResizeMethod.Method.stretch)
            {
                int sourceWidth = img.Width;
                int sourceHeight = img.Height;
                int sourceX = 0;
                int sourceY = 0;

                int destX = 0;
                int destY = 0;
                
                Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                         PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(img.HorizontalResolution,
                                        img.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                

                grPhoto.DrawImage(img,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);

                grPhoto.Dispose();

                out_img = bmPhoto;
            }
            else 
            {
                log.error("Unsupported resize method: {0}", method);
                throw new Exception(string.Format("Internal error, unsupported resize method: {0}", method));
            }

            ImageCodecInfo ici = new ImageCodecInfo();
            ici.MimeType 
            out_img.Save(Path.Combine(output_folder, Path.GetFileName(input_path)), );
            img.Dispose();
            out_img.Dispose();
        }

        ProfileHandler.Profile get_current_params()
        {
            ProfileHandler.Profile p;

            try
            {
                p = new ProfileHandler.Profile("dummy",
                    ((ResizeMethod)resizeMethodCmBx.SelectedItem).method,
                    Convert.ToInt32(widthTB.Text),
                    Convert.ToInt32(heightTB.Text),
                    false);

                p.input_folder = this.inputFolderTB.Text;
                p.output_folder = this.outputFolderTB.Text;
            }
            catch (Exception ex)
            {
                log.error("Unable to obtain current conversion parameters to return into the worker thread");
                log.debug(ex.ToString());
                p = null;
            }
            return p;
        }

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
            string[] files = Directory.GetFiles(conversion_params.input_folder);

            log.debug("Located {0} files on input directory");

            int file_count = files.Length;

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
            startButton.Text = get_lang_string("dialog_startButton_start");

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
        }

        private void showLoggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", this.log.get_log_filename());
        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = ((ToolStripMenuItem)sender);
            string lang = mi.Tag as string;
            set_language(lang);

            Settings.Default.language = lang;
            Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
