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

            SimpleLogger.LogLevel level;
#if DEBUG
            test_params = true;
            level = SimpleLogger.LogLevel.debug;
#else
            test_params = false;
            level = SimpleLogger.LogLevel.info;
#endif

            log = new SimpleLogger("image_resizer.log", level);

            GuiInitialization();

#if DEBUG
            inputFolderTB.Text = @"C:\Users\tonio\Pictures\FotosDeLaVillaForExperiments";
            outputFolderTB.Text = @"C:\Users\tonio\Pictures\FotosDeLaVillaForExperiments\out";
#endif
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
        public bool test_params;

        ResizeMethods methods;

        private int bulk_test_current_index = -1;
        private string bulk_test_base_folder = null;

        TextShower resizeInfoWindow = null;
        TextShower releaseNotesWindow = null;
        string release_notes_file = null;

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

            string text = "";
            foreach (ResizeMethod rm in methods.methods)
            {
                rm.reload_translation();
                text += string.Format("{0}\r\n", rm.short_name);
                text += string.Format("{0}\r\n\r\n", rm.description);
            }
            this.resizeInfoWindow.set_text(text);
            
            // Custom controls
            // Need to clear and add again the items to ensure the string representation is re-queried
            // There seems to be a best method but requires implementing INotifyPropertyChanged.
            int selected_index = this.resizeMethodCmBx.SelectedIndex;
            this.resizeMethodCmBx.Items.Clear();
            foreach (ResizeMethod m in methods.methods)
            {
                this.resizeMethodCmBx.Items.Add(m);
            }
            this.resizeMethodCmBx.SelectedIndex = selected_index;

            if (outFolAutoSelCB.Checked)
            {
                update_auto_folder_selection();
            }
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
                return;
            }
            log.debug("Loading profile {0}", profile.name);

            setResizeMetdhod(profile.method);
            widthTB.Text = profile.width.ToString();
            heightTB.Text = profile.height.ToString();

            Settings.Default.activeProfile = profile.name;
            Settings.Default.Save();
        }

        public string get_lang_string(string name)
        {
            string strn;
            try
            {
                strn = this.resourceManager.GetString(name, this.culture);
            }
            catch (Exception ex)
            {
                strn = string.Format("NotAvailStrn: '{0}'", name);
                this.log.error("Unable to find string for translation '{0}' on culture '{1}'", name, this.culture.Name);
                log.debug(ex.ToString());
            }
            this.log.debug("Get language string: '{0}': '{1}'", name, strn);
            return strn;
        }

        private void cancel_operation()
        {
            log.info("Cancelling work");
            backgroundProcessor.CancelAsync();

            startButton.Text = get_lang_string("gui_dialog_startButton_cancelling");
            state = WorkState.canceling;
        }

        private void start_operation()
        {
            log.debug("Starting operation...");

            progressBar.Maximum = 100;
            progressBar.Minimum = 0;

            startButton.Text = get_lang_string("gui_dialog_startButton_cancel");
            state = WorkState.running;

            backgroundProcessor.RunWorkerAsync();
        }

        /* Returns the current conversion parameters. Called from the background worker.
         */
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
    }
}
