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
using System.Resources;
using ImageResizer.Properties;
using System.Reflection;

namespace ImageResizer
{
    public partial class mainForm : Form
    {
        private void GuiInitialization()
        {
            this.resourceManager = new ResourceManager("ImageResizer.Resource.Res", typeof(mainForm).Assembly);

            // Cannot be set on the designer
            this.inputFolderGroupBox.AllowDrop = true;

            // Has a default text on the designer
            this.OutputFolderWarningLabel.Text = "";

            this.state = WorkState.idle;

            this.methods = new ResizeMethods(this);
            this.resizeInfoWindow = new TextShower();

            // Release notes
            this.release_notes_file = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "content"), "release_notes.txt");
            if (File.Exists(release_notes_file))
            {
                this.releaseNotesWindow = new TextShower();
                log.debug("Loading release notes from {0}", this.release_notes_file);
                this.releaseNotesWindow.set_text(File.ReadAllText(this.release_notes_file));
            }
            else {
                log.error("Release notes file does not exist!: {0}", release_notes_file);
            }

            // Bulk test is only left in place if this is a build with test parameters
            if (!this.test_params)
            {
                toolsToolStripMenuItem.DropDownItems.RemoveByKey("doBulkRunToolStripMenuItem");
            }

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
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.resizeMethodsHelpToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.releaseNotesToolStripMenuItem));
            this.translatable_gui_items.Add(new TGI_Control(this, this.resizeInfoWindow, "resizeInfoWindow"));
            this.translatable_gui_items.Add(new TGI_Control(this, this.startButton, "dialog_startButton_start"));
            if (this.releaseNotesWindow != null)
            {
                // This control will be null when the release notes file is not found
                this.translatable_gui_items.Add(new TGI_Control(this, this.releaseNotesWindow, "releaseNotesWindow"));
            }
            this.translatable_gui_items.Add(new TGI_ToolStripMenuItem(this, this.helpToolStripMenuItem));

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

            events_enabled = true;
        }

        #region Event Handlers
        private void showLoggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = this.log.get_log_filename();
            Process.Start("explorer.exe", string.Format("/select,{0}", filename));
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
            if (this.outFolAutoSelCB.Checked)
            {
                update_auto_folder_selection();
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
                start_operation();
            }
            else if (state == WorkState.running)
            {
                cancel_operation();
            }
            else if (state == WorkState.canceling)
            {
                log.warning("Cancellation already in progress...");
            }
            else
            {
                throw new Exception(string.Format("Unknown work state: {0}", state));
            }
        }

        private void do_bulk_test_work()
        {
            log.debug("do_bulk_test_work function call");

            if (state != WorkState.idle)
            {
                log.warning("do_bulk_test_work was called when state was not idle!");
                return;
            }

            if (this.bulk_test_current_index == -1)
            {
                this.bulk_test_base_folder = this.outputFolderTB.Text;
                this.bulk_test_current_index = 0;
            }
            else if (this.bulk_test_current_index == methods.methods.Count)
            {
                this.bulk_test_current_index = -1;
                this.outputFolderTB.Text = this.bulk_test_base_folder;
                log.debug("bulk test finished, latest index was reached");
                return;
            }

            log.debug("bulk operation for index {0} with method {1}", this.bulk_test_current_index, this.methods.methods[bulk_test_current_index].short_name);

            this.outputFolderTB.Text = Path.Combine(this.bulk_test_base_folder, this.methods.methods[bulk_test_current_index].short_name);
            this.setResizeMetdhod(this.methods.methods[bulk_test_current_index].method);

            start_operation();

            this.bulk_test_current_index++;
        }

        private void doBulkRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            do_bulk_test_work();
        }

        private void resizeMethodsHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.resizeInfoWindow.Show();
        }

        private void outFolAutoSelCB_CheckedChanged(object sender, EventArgs e)
        {
            if (outFolAutoSelCB.Checked)
            {
                update_auto_folder_selection();
                this.outputFolderTB.ReadOnly = true;
            }
            else
            {
                this.OutputFolderWarningLabel.Text = "";
                this.outputFolderTB.ReadOnly = false;
            }
        }

        private void releaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.releaseNotesWindow != null)
            {
                this.releaseNotesWindow.Show();
            }
            else
            {
                MessageBox.Show(string.Format("{0}:\r\n{1}", this.get_lang_string("dialog_release_notes_missing"), this.release_notes_file),
                    this.get_lang_string("dialog_release_notes_missing_caption"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tool_name = null;
            string publisher = "";
            string version = String.Format("{1}{0}", 
                Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                this.get_lang_string("dialog_about_version_prefix"));
            DateTime last_written = File.GetLastWriteTime(Application.ExecutablePath);
            string date;
            try
            {
                date = string.Format("{0} {1}",
                    this.get_lang_string("dialog_about_date_prefix"),
                    last_written.ToString(this.get_lang_string("dialog_about_date_string_format")));
            }
            catch(Exception ex)
            {
                log.error("Failed to format date for About Window");
                log.debug(ex.ToString());
                date = last_written.ToString() + " (Default Format)";
            }
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != "")
                {
                    tool_name = titleAttribute.Title;
                }
                
            }
            attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length != 0)
            {
                publisher = String.Format("{1} {0}", 
                    ((AssemblyCompanyAttribute)attributes[0]).Company,
                    this.get_lang_string("dialog_about_publisher_prefix"));
            }
            if (tool_name == null)
            {
                tool_name = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }

            AboutForm aboutWindow = new AboutForm(string.Format("{0} {1}", this.get_lang_string("gui_aboutToolStripMenuItem"), tool_name),
                tool_name,
                publisher,
                version,
                date,
                "https://github.com/tonioluna/imageResizer");

            aboutWindow.ShowDialog();
        }

        private void OnDragEnter_InputFolder(object sender, System.Windows.Forms.DragEventArgs e)
        {
            log.debug("OnDragEnter");
            string folder = GetFolder(e);
            if (folder != null)
            {
                e.Effect = DragDropEffects.Copy;
                //this.inputFolderTB.Text = folder;
                //this.outFolAutoSelCB.Checked = false;
                //this.outFolAutoSelCB.Checked = true;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnDragDrop_InputFolder(object sender, DragEventArgs e)
        {
            log.debug("OnDragEnter");
            string folder = GetFolder(e);
            if (folder != null)
            {
                e.Effect = DragDropEffects.Copy;
                this.inputFolderTB.Text = folder;
                this.outFolAutoSelCB.Checked = false;
                this.outFolAutoSelCB.Checked = true;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        #endregion Event Handlers

        protected string GetFolder(DragEventArgs e)
        {
            string folder = null;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileName") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        string temp = ((string[])data)[0];
                        temp = Path.GetFullPath(temp);
                        if (Directory.Exists(temp))
                        {
                            folder = temp;
                        }
                    }
                }
            }
            return folder;
        }

        void update_auto_folder_selection()
        {
            string input_folder = this.inputFolderTB.Text;
            if (Directory.Exists(input_folder))
            {
                string auto_folder = Path.Combine(input_folder, this.get_lang_string("dialog_default_output_folder"));
                if (Directory.Exists(auto_folder))
                {
                    int index = 2;
                    string format_string = this.get_lang_string("dialog_default_output_folder_with_index");
                    while (true)
                    {
                        auto_folder = auto_folder = Path.Combine(input_folder, string.Format(format_string, index));
                        if (!Directory.Exists(auto_folder))
                        {
                            break;
                        }
                        index++;
                    }
                    this.OutputFolderWarningLabel.Text = this.get_lang_string("dialog_default_output_exists");
                }
                else
                {
                    this.OutputFolderWarningLabel.Text = "";
                }

                this.outputFolderTB.Text = auto_folder;
            }
            else
            {
                this.OutputFolderWarningLabel.Text = this.get_lang_string("dialog_auto_output_folder_input_err");
            }

        }
    }
}
