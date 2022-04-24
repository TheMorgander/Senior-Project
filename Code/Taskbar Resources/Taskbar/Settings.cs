﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskbar
{
    public class Settings : INotifyPropertyChanged
    {
        public bool layout_cpu_enabled
        {
            get { return bool.Parse(GetValue("layout_cpu_enabled")); }
            set { SetValue("layout_cpu_enabled", value.ToString()); OnPropertyChanged("layout_cpu_enabled"); }
        }
        public bool layout_gpu_enabled
        {
            get { return bool.Parse(GetValue("layout_gpu_enabled")); }
            set { SetValue("layout_gpu_enabled", value.ToString()); OnPropertyChanged("layout_gpu_enabled"); }
        }
        public bool layout_ram_enabled
        {
            get { return bool.Parse(GetValue("layout_ram_enabled")); }
            set { SetValue("layout_ram_enabled", value.ToString()); OnPropertyChanged("layout_ram_enabled"); }
        }
        public bool layout_disk_enabled
        {
            get { return bool.Parse(GetValue("layout_disk_enabled")); }
            set { SetValue("layout_disk_enabled", value.ToString()); OnPropertyChanged("layout_disk_enabled"); }
        }
        public bool layout_network_enabled
        {
            get { return bool.Parse(GetValue("layout_network_enabled")); }
            set { SetValue("layout_network_enabled", value.ToString()); OnPropertyChanged("layout_network_enabled"); }
        }

        public bool layout_header_enabled
        {
            get { return bool.Parse(GetValue("layout_header_enabled")); }
            set { SetValue("layout_header_enabled", value.ToString()); OnPropertyChanged("layout_header_enabled"); }
        }
        public bool layout_header_bold
        {
            get { return bool.Parse(GetValue("layout_header_bold")); }
            set { SetValue("layout_header_bold", value.ToString()); OnPropertyChanged("layout_header_bold"); }
        }
        public bool layout_header_italic
        {
            get { return bool.Parse(GetValue("layout_header_italic")); }
            set { SetValue("layout_header_italic", value.ToString()); OnPropertyChanged("layout_header_italic"); }
        }
        public bool layout_header_underline
        {
            get { return bool.Parse(GetValue("layout_header_underline")); }
            set { SetValue("layout_header_underline", value.ToString()); OnPropertyChanged("layout_header_underline"); }
        }

        public string layout_cpu_color
        {
            get { return GetValue("layout_cpu_color"); }
            set { SetValue("layout_cpu_color", value.ToString()); OnPropertyChanged("layout_cpu_color"); }
        }
        public bool layout_cpu_bold
        {
            get { return bool.Parse(GetValue("layout_cpu_bold")); }
            set { SetValue("layout_cpu_bold", value.ToString()); OnPropertyChanged("layout_cpu_bold"); }
        }
        public bool layout_cpu_italic
        {
            get { return bool.Parse(GetValue("layout_cpu_italic")); }
            set { SetValue("layout_cpu_italic", value.ToString()); OnPropertyChanged("layout_cpu_italic"); }
        }
        public bool layout_cpu_underline
        {
            get { return bool.Parse(GetValue("layout_cpu_underline")); }
            set { SetValue("layout_cpu_underline", value.ToString()); OnPropertyChanged("layout_cpu_underline"); }
        }

        public string layout_gpu_color
        {
            get { return GetValue("layout_gpu_color"); }
            set { SetValue("layout_gpu_color", value.ToString()); OnPropertyChanged("layout_gpu_color"); }
        }
        public bool layout_gpu_bold
        {
            get { return bool.Parse(GetValue("layout_gpu_bold")); }
            set { SetValue("layout_gpu_bold", value.ToString()); OnPropertyChanged("layout_gpu_bold"); }
        }
        public bool layout_gpu_italic
        {
            get { return bool.Parse(GetValue("layout_gpu_italic")); }
            set { SetValue("layout_gpu_italic", value.ToString()); OnPropertyChanged("layout_gpu_italic"); }
        }
        public bool layout_gpu_underline
        {
            get { return bool.Parse(GetValue("layout_gpu_underline")); }
            set { SetValue("layout_gpu_underline", value.ToString()); OnPropertyChanged("layout_gpu_underline"); }
        }

        public string layout_ram_color
        {
            get { return GetValue("layout_ram_color"); }
            set { SetValue("layout_ram_color", value.ToString()); OnPropertyChanged("layout_ram_color"); }
        }
        public bool layout_ram_bold
        {
            get { return bool.Parse(GetValue("layout_ram_bold")); }
            set { SetValue("layout_ram_bold", value.ToString()); OnPropertyChanged("layout_ram_bold"); }
        }
        public bool layout_ram_italic
        {
            get { return bool.Parse(GetValue("layout_ram_italic")); }
            set { SetValue("layout_ram_italic", value.ToString()); OnPropertyChanged("layout_ram_italic"); }
        }
        public bool layout_ram_underline
        {
            get { return bool.Parse(GetValue("layout_ram_underline")); }
            set { SetValue("layout_ram_underline", value.ToString()); OnPropertyChanged("layout_ram_underline"); }
        }

        public string layout_disk_color
        {
            get { return GetValue("layout_disk_color"); }
            set { SetValue("layout_disk_color", value.ToString()); OnPropertyChanged("layout_disk_color"); }
        }
        public bool layout_disk_bold
        {
            get { return bool.Parse(GetValue("layout_disk_bold")); }
            set { SetValue("layout_disk_bold", value.ToString()); OnPropertyChanged("layout_disk_bold"); }
        }
        public bool layout_disk_italic
        {
            get { return bool.Parse(GetValue("layout_disk_italic")); }
            set { SetValue("layout_disk_italic", value.ToString()); OnPropertyChanged("layout_disk_italic"); }
        }
        public bool layout_disk_underline
        {
            get { return bool.Parse(GetValue("layout_disk_underline")); }
            set { SetValue("layout_disk_underline", value.ToString()); OnPropertyChanged("layout_disk_underline"); }
        }

        public string layout_network_color
        {
            get { return GetValue("layout_network_color"); }
            set { SetValue("layout_network_color", value.ToString()); OnPropertyChanged("layout_network_color"); }
        }
        public bool layout_network_bold
        {
            get { return bool.Parse(GetValue("layout_network_bold")); }
            set { SetValue("layout_network_bold", value.ToString()); OnPropertyChanged("layout_network_bold"); }
        }
        public bool layout_network_italic
        {
            get { return bool.Parse(GetValue("layout_network_italic")); }
            set { SetValue("layout_network_italic", value.ToString()); OnPropertyChanged("layout_network_italic"); }
        }
        public bool layout_network_underline
        {
            get { return bool.Parse(GetValue("layout_network_underline")); }
            set { SetValue("layout_network_underline", value.ToString()); OnPropertyChanged("layout_network_underline"); }
        }

        /* Value change listener */
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private static Dictionary<string, string> settings = new Dictionary<string, string>();

        public static void Initalize()
        {
            if (File.Exists("config.json"))
            {
                ReadConfig();
            }
            else
            {
                File.Create("config.json").Dispose();
                Default();
                WriteConfig();
            }
        }

        public static void SetValue(string key, string value)
        {
            settings[key] = value;
            WriteConfig();
        }

        public static string GetValue(string key)
        {
            return settings[key];
        }

        private static void ReadConfig()
        {
            string json = File.ReadAllText(@"config.json");
            settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        private static void WriteConfig()
        {
            string json = JsonConvert.SerializeObject(settings);
            System.IO.File.WriteAllText(@"config.json", json);
        }

        private static void Default()
        {
            settings.Add("layout_cpu_enabled", "True");
            settings.Add("layout_gpu_enabled", "True");
            settings.Add("layout_ram_enabled", "True");
            settings.Add("layout_disk_enabled", "True");
            settings.Add("layout_network_enabled", "True");

            settings.Add("layout_header_enabled", "True");
            settings.Add("layout_header_bold", "False");
            settings.Add("layout_header_italic", "False");
            settings.Add("layout_header_underline", "False");

            settings.Add("layout_cpu_color", "White");
            settings.Add("layout_cpu_bold", "False");
            settings.Add("layout_cpu_italic", "False");
            settings.Add("layout_cpu_underline", "False");

            settings.Add("layout_gpu_color", "White");
            settings.Add("layout_gpu_bold", "False");
            settings.Add("layout_gpu_italic", "False");
            settings.Add("layout_gpu_underline", "False");

            settings.Add("layout_ram_color", "White");
            settings.Add("layout_ram_bold", "False");
            settings.Add("layout_ram_italic", "False");
            settings.Add("layout_ram_underline", "False");

            settings.Add("layout_disk_color", "White");
            settings.Add("layout_disk_bold", "False");
            settings.Add("layout_disk_italic", "False");
            settings.Add("layout_disk_underline", "False");

            settings.Add("layout_network_color", "White");
            settings.Add("layout_network_bold", "False");
            settings.Add("layout_network_italic", "False");
            settings.Add("layout_network_underline", "False");
        }
    }
}
