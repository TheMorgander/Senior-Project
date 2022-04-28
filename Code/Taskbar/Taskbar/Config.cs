using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskbar;

namespace Taskbar
{
    public class Config : INotifyPropertyChanged
    {
        /******************************************************************/
        #region General Variables
        private Dictionary<string, dynamic> settings = new Dictionary<string, dynamic>();
        private string config_file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/config.json";
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Watched Variables
        protected internal bool layout_cpu_enabled
        {
            get { return GetValue("layout_cpu_enabled"); }
            set { SetValue("layout_cpu_enabled", value); }
        }
        protected internal bool layout_gpu_enabled
        {
            get { return GetValue("layout_gpu_enabled"); }
            set { SetValue("layout_gpu_enabled", value); }
        }
        protected internal bool layout_ram_enabled
        {
            get { return GetValue("layout_ram_enabled"); }
            set { SetValue("layout_ram_enabled", value); }
        }
        protected internal bool layout_disk_enabled
        {
            get { return GetValue("layout_disk_enabled"); }
            set { SetValue("layout_disk_enabled", value); }
        }
        protected internal bool layout_network_enabled
        {
            get { return GetValue("layout_network_enabled"); }
            set { SetValue("layout_network_enabled", value); }
        }
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Variable Listener
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        /******************************************************************/

        /******************************************************************/
        protected internal void Initalize()
        {
            if (File.Exists(config_file))
            {
                ReadConfig();
            }
            else
            {
                CreateConfig();
                WriteConfig();
            }
        }
        /******************************************************************/

        /******************************************************************/
        protected internal void SetValue(string key, dynamic value)
        {
            try
            {
                settings[key] = value;
                OnPropertyChanged(key);
                WriteConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Taskbar Resources encountered an error:\n" +
                ex.Message + "\n" +
                ex.StackTrace + "\n\n");
            }
        }
        /******************************************************************/

        /******************************************************************/
        protected internal dynamic GetValue(string key)
        {
            try
            {
                return settings[key];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Taskbar Resources encountered an error:\n" +
                ex.Message + "\n" +
                ex.StackTrace + "\n\n");
                return null;
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void ReadConfig()
        {
            try
            {
                string json = File.ReadAllText(config_file);
                settings = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Taskbar Resources encountered an error:\n" +
                ex.Message + "\n" +
                ex.StackTrace + "\n\n");
                CreateConfig();
                WriteConfig();
            }   
        }
        /******************************************************************/

        /******************************************************************/
        private void WriteConfig()
        {
            try
            {
                string json = JsonConvert.SerializeObject(settings);
                File.WriteAllText(config_file, json);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Taskbar Resources encountered an error:\n" +
                ex.Message + "\n" +
                ex.StackTrace + "\n\n");
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void CreateConfig()
        {
            try
            {
                File.Create(config_file).Dispose();

                settings.Clear();

                settings.Add("layout_cpu_enabled", true);
                settings.Add("layout_gpu_enabled", true);
                settings.Add("layout_ram_enabled", true);
                settings.Add("layout_disk_enabled", true);
                settings.Add("layout_network_enabled", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Taskbar Resources encountered an error:\n" +
                ex.Message + "\n" +
                ex.StackTrace + "\n\n");
            }
        }
        /******************************************************************/
    }
}
