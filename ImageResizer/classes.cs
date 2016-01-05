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
        public SimpleLogger(string filename, LogLevel fileLevel)
        {
            this.fileLevel = fileLevel;
            
            filename = string.Format("{0}.{1}.log", Path.GetFileNameWithoutExtension(filename), Process.GetCurrentProcess().Id);
            string folder_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
            if (!Directory.Exists(folder_name))
            {
                Directory.CreateDirectory(folder_name);
            }
            filename = Path.Combine(folder_name, filename);
            this.log_filename = filename;
            this.handler = new StreamWriter(filename, false);

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            this.info("Starting application on {0}. Version: {1}.", DateTime.Now.ToLongDateString(), version);
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
        public ResizeMethods()
        {
            this.methods = new List<ResizeMethod>();
            this.methods.Add(new ResizeMethod(ResizeMethod.Method.add_fill, "Add Fill Margings", "Resize image to fit on box keeping aspect ratio and add filling margings to make up required size. Output image will always be the target dimmensions."));
            this.methods.Add(new ResizeMethod(ResizeMethod.Method.cut_excess, "Cut Excess", "Resize image keeping aspect ratio until it fully covers the output dimmensions. Then excess is trimmed. Output image will always be the target dimmensions."));
            this.methods.Add(new ResizeMethod(ResizeMethod.Method.fit_on_box, "Fit on Box", "Resize image to fit box keeping aspect ratio. Output image will be equal or smaller to target dimmensions."));
            this.methods.Add(new ResizeMethod(ResizeMethod.Method.stretch, "Stretch to Box", "Resize image to fit box not keeping aspect ratio. Output image will always be the target dimmensions."));
        }
        public List<ResizeMethod> methods;
    }

    public class ResizeMethod
    {
        public ResizeMethod(Method method, string short_name, string desc)
        {
            this.method = method;
            this.short_name = short_name;
            this.description = desc;
        }

        public override string ToString()
        {
            return this.short_name;
        }

        public Method method;
        public string short_name;
        public string description;

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
            this.profiles.Add(new Profile("Digital Frame", ResizeMethod.Method.fit_on_box, 1024, 768, true));
            this.profiles.Add(new Profile("Full HD", ResizeMethod.Method.cut_excess, 1920, 1080, true));

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
        }
        Control control;

        public override void reload_text()
        {
            string strn = this.mf.resourceManager.GetString(string.Format("gui_{0}", this.control.Name), this.mf.culture);
            this.mf.log.debug("Set Control text for '{0}': '{1}'", this.control.Name, strn);
            this.control.Text = "*" + strn;
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
            string strn = this.mf.resourceManager.GetString(string.Format("gui_{0}", this.tsmi.Name), this.mf.culture);
            this.mf.log.debug("Set Menu Item text for '{0}': '{1}'", this.tsmi.Name, strn);
            this.tsmi.Text = "*" + strn;
        }
    }

    

    public enum WorkState { 
        idle,
        running,
        canceling
    }
}
