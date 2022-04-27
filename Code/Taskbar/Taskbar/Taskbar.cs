﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Settings;

namespace Taskbar
{
    public partial class Taskbar : Form
    {
        /******************************************************************/
        #region General Variables
        public static Taskbar taskbar;

        public readonly History history;
        public readonly Config config;
        public readonly Resources resources;
        #endregion
        /******************************************************************/

        /******************************************************************/
        internal protected Taskbar(CSDeskBand.CSDeskBandWin window)
        {
            try
            {
                taskbar = this;
                //history = new History();
                //history.Initalize();
                config = new Config();
                config.Initalize();
                resources = new Resources();
                resources.Initalize();

                InitializeComponent();

                cpuValue.DataBindings.Add(new Binding("Text", resources, "cpu_usage_string", true, DataSourceUpdateMode.OnPropertyChanged));
                cpuValue.DataBindings.Add(new Binding("Enabled", config, "layout_cpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                cpuValue.DataBindings.Add(new Binding("Visible", config, "layout_cpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                gpuValue.DataBindings.Add(new Binding("Text", resources, "gpu_usage_string", true, DataSourceUpdateMode.OnPropertyChanged));
                gpuValue.DataBindings.Add(new Binding("Enabled", config, "layout_gpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                gpuValue.DataBindings.Add(new Binding("Visible", config, "layout_gpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                ramValue.DataBindings.Add(new Binding("Text", resources, "ram_usage_string", true, DataSourceUpdateMode.OnPropertyChanged));
                ramValue.DataBindings.Add(new Binding("Enabled", config, "layout_ram_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                ramValue.DataBindings.Add(new Binding("Visible", config, "layout_ram_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                diskReadValue.DataBindings.Add(new Binding("Text", resources, "disk_read_string", true, DataSourceUpdateMode.OnPropertyChanged));
                diskReadValue.DataBindings.Add(new Binding("Enabled", config, "layout_disk_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                diskReadValue.DataBindings.Add(new Binding("Visible", config, "layout_disk_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                
                diskWriteValue.DataBindings.Add(new Binding("Text", resources, "disk_write_string", true, DataSourceUpdateMode.OnPropertyChanged));
                diskWriteValue.DataBindings.Add(new Binding("Enabled", config, "layout_disk_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                diskWriteValue.DataBindings.Add(new Binding("Visible", config, "layout_disk_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                
                networkUploadValue.DataBindings.Add(new Binding("Text", resources, "network_upload_string", true, DataSourceUpdateMode.OnPropertyChanged));
                networkUploadValue.DataBindings.Add(new Binding("Enabled", config, "layout_network_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                networkUploadValue.DataBindings.Add(new Binding("Visible", config, "layout_network_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                
                networkDownloadValue.DataBindings.Add(new Binding("Text", resources, "network_download_string", true, DataSourceUpdateMode.OnPropertyChanged));
                networkDownloadValue.DataBindings.Add(new Binding("Enabled", config, "layout_network_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                networkDownloadValue.DataBindings.Add(new Binding("Visible", config, "layout_network_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                cpuHeader.DataBindings.Add(new Binding("Enabled", config, "layout_cpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                cpuHeader.DataBindings.Add(new Binding("Visible", config, "layout_cpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                gpuHeader.DataBindings.Add(new Binding("Enabled", config, "layout_gpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                gpuHeader.DataBindings.Add(new Binding("Visible", config, "layout_gpu_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                ramHeader.DataBindings.Add(new Binding("Enabled", config, "layout_ram_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                ramHeader.DataBindings.Add(new Binding("Visible", config, "layout_ram_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                diskHeader.DataBindings.Add(new Binding("Enabled", config, "layout_disk_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                diskHeader.DataBindings.Add(new Binding("Visible", config, "layout_disk_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                networkHeader.DataBindings.Add(new Binding("Enabled", config, "layout_network_enabled", true, DataSourceUpdateMode.OnPropertyChanged));
                networkHeader.DataBindings.Add(new Binding("Visible", config, "layout_network_enabled", true, DataSourceUpdateMode.OnPropertyChanged));

                MainWindow main = new MainWindow();
                ElementHost.EnableModelessKeyboardInterop(main);
                main.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        /******************************************************************/
    }
}
