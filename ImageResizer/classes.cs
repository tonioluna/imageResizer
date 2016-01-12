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

namespace ImageResizer
{
    public class SimpleLogger
    {
        public SimpleLogger(string base_filename, LogLevel fileLevel)
        {
            this.fileLevel = fileLevel;
            
            string filename = string.Format("{0}.{1}.log", Path.GetFileNameWithoutExtension(base_filename), Process.GetCurrentProcess().Id);
            string folder_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
            if (!Directory.Exists(folder_name))
            {
                Directory.CreateDirectory(folder_name);
            }
            else
            { 
                // if folder exists then clean old logs
                this.clean_old_logs(base_filename, folder_name);
            }
            filename = Path.Combine(folder_name, filename);
            this.log_filename = filename;
            this.handler = new StreamWriter(filename, false);

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            this.info("Starting application on {0}. Version: {1}.", DateTime.Now.ToLongDateString(), version);
        }

        /* Remove any logfile older than 24 hrs
         */
        void clean_old_logs(string base_filename, string folder)
        {
            base_filename = Path.GetFileNameWithoutExtension(base_filename);
            string [] files = Directory.GetFiles(folder);

            DateTime now = DateTime.Now;
            TimeSpan ts_24hrs = TimeSpan.FromHours(24.0);

            foreach (string file in files) {
                string ffile = Path.Combine(folder, file);
                DateTime last_access = File.GetLastWriteTime(ffile);

                TimeSpan delta = now - last_access;

                if (delta > ts_24hrs)
                {
                    try
                    {
                        File.Delete(ffile);
                    }
                    catch { }
                }
            }
        }

        public LogLevel fileLevel;
        private string log_filename;
        private StreamWriter handler;

        public string get_log_filename() {
            return this.log_filename;
        }

        public enum LogLevel { 
            debug = 0,
            info = 1,
            warning = 2,
            error = 3
        }

        private void raw_writter(string message, string level, params object[] args) 
        {
            if (args.Length != 0)
            {
                message = string.Format(message, args);
            }
            this.handler.Write(string.Format("{0} {1,10} : {2}\r\n", DateTime.Now.ToLongTimeString(), level, message));
            this.handler.Flush();
        }

        public void error(string message, params object[] args)
        {
            if (this.fileLevel <= LogLevel.error)
                this.raw_writter(message, "Error", args);
        }

        public void warning(string message, params object[] args)
        {
            if (this.fileLevel <= LogLevel.warning)
                this.raw_writter(message, "Warning", args);
        }

        public void info(string message, params object[] args)
        {
            if (this.fileLevel <= LogLevel.info)
                this.raw_writter(message, "Info", args);
        }

        public void debug(string message, params object[] args)
        {
            if (this.fileLevel <= LogLevel.debug)
                this.raw_writter(message, "Debug", args);
        }
    }

    public class ResizeMethods
    {
        public ResizeMethods(mainForm mf)
        {
            this.methods = new List<ResizeMethod>();
            this.mf = mf;
            //this.methods.Add(new ResizeMethod(ResizeMethod.Method.add_fill, "add_fill", mf));
            this.methods.Add(new ResizeMethod(ResizeMethod.Method.cut_excess, "cut_excess", mf));
            this.methods.Add(new ResizeMethod(ResizeMethod.Method.fit_on_box, "fit_on_box", mf));
            this.methods.Add(new ResizeMethod(ResizeMethod.Method.stretch, "stretch_to_box", mf));
        }
        public List<ResizeMethod> methods;
        private mainForm mf;
    }

    public class ResizeMethod
    {
        public ResizeMethod(Method method, string id, mainForm mf)
        {
            this.method = method;
            this.id = id;
            this.mf = mf;
            this.reload_translation();
        }

        public void reload_translation()
        {
            this.short_name = this.mf.get_lang_string(string.Format("method_short_{0}", this.id));
            this.description = this.mf.get_lang_string(string.Format("method_desc_{0}", this.id));
        }

        public override string ToString()
        {
            return this.short_name;
        }

        public Method method;
        public string short_name;
        public string description;
        public string id;
        private mainForm mf;

        public enum Method
        {
            cut_excess,
            stretch,
            add_fill,
            fit_on_box,
        };
    }

    public class ProfileHandler 
    {
        public ProfileHandler()
        {
            // Load static profiles
            this.profiles = new List<Profile>();
            this.profiles.Add(new Profile("eMotion Digital Frame", ResizeMethod.Method.fit_on_box, 800, 600, true));
            this.profiles.Add(new Profile("Full HD", ResizeMethod.Method.fit_on_box, 1920, 1080, true));

            // TODO: support loading external profiles

            // Create the profile dictionary
            this.prof_dict = new Dictionary<string, Profile>();
            foreach (Profile p in this.profiles)
            {
                this.prof_dict[p.name] = p;
            }
        }

        private List<Profile> profiles;
        private Dictionary<String, Profile> prof_dict;

        public Profile get_profile_by_name(string name)
        {
            return this.prof_dict.ContainsKey(name) ? this.prof_dict[name] : null;
        }

        public List<Profile> get_profiles()
        {
            List<Profile> p = new List<Profile>();
            p.AddRange(this.profiles);

            return p;
        }

        public class Profile
        { 
            public Profile(string name, ResizeMethod.Method method, int width, int height, bool is_fixed)
            {
                this.name = name;
                this.method = method;
                this.width = width;
                this.height = height;
                this.is_fixed = is_fixed; // To indicate which profiles can be removed and which are fixed on the application
            }
            public string name;
            public ResizeMethod.Method method;
            public int width;
            public int height;
            public bool is_fixed;

            // These are used when this class is used to pass data within threads
            public string input_folder;
            public string output_folder;

            public override string ToString()
            {
                return this.name;
            }
        }   
    }

    public class TranslatableGuiItem
    {
        public TranslatableGuiItem(mainForm mf)
        {
            this.mf = mf;
        }
        public mainForm mf;

        public virtual void reload_text()
        {
            throw new NotImplementedException();
        }
    }

    public class TGI_Control : TranslatableGuiItem
    {
        public TGI_Control(mainForm mf, Control control)
            : base(mf)
        {
            this.control = control;
            this.alternate_source = null;
        }
        public TGI_Control(mainForm mf, Control control, string alternate_source)
            : base(mf)
        {
            this.control = control;
            this.alternate_source = alternate_source;
        }

        Control control;
        string alternate_source;

        public override void reload_text()
        {
            string strn;
            if (alternate_source != null)
            {
                strn = this.mf.get_lang_string(string.Format("gui_{0}", this.alternate_source));
                this.mf.log.debug("Set Control text for '{0}' with alternate source '{2}': '{1}'", this.control.Name, strn, this.alternate_source);
            }
            else
            {
                strn = this.mf.get_lang_string(string.Format("gui_{0}", this.control.Name));
                this.mf.log.debug("Set Control text for '{0}': '{1}'", this.control.Name, strn);
            }
            this.control.Text = strn;
        }
    }

    public class TGI_ToolStripMenuItem : TranslatableGuiItem
    {
        public TGI_ToolStripMenuItem(mainForm mf, ToolStripMenuItem tsmi)
            : base(mf)
        {
            this.tsmi = tsmi;
        }
        ToolStripMenuItem tsmi;
        public override void reload_text()
        {
            //string strn = this.mf.resourceManager.GetString(string.Format("gui_{0}", this.tsmi.Name), this.mf.culture);
            string strn = this.mf.get_lang_string(string.Format("gui_{0}", this.tsmi.Name));
            this.mf.log.debug("Set Menu Item text for '{0}': '{1}'", this.tsmi.Name, strn);
            this.tsmi.Text = strn;
        }
    }

    

    public enum WorkState { 
        idle,
        running,
        canceling
    }
}
