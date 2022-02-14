using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taskbar
{
    public partial class Taskbar : Form
    {
        public Resources resources = new Resources();
        public Settings settings = new Settings();

        public Taskbar()
        {
            /* Start collecting resources */
            resources.GetResources(500);

            /* Initalize winform layout */
            InitializeComponent();

            /* Bind display labels to resource values */
            cpuValue.DataBindings.Add(new Binding("Text", resources, "cpu_value", true, DataSourceUpdateMode.OnPropertyChanged));
            gpuValue.DataBindings.Add(new Binding("Text", resources, "gpu_value", true, DataSourceUpdateMode.OnPropertyChanged));
            ramValue.DataBindings.Add(new Binding("Text", resources, "ram_value", true, DataSourceUpdateMode.OnPropertyChanged));
            diskReadValue.DataBindings.Add(new Binding("Text", resources, "disk_read_value", true, DataSourceUpdateMode.OnPropertyChanged));
            diskWriteValue.DataBindings.Add(new Binding("Text", resources, "disk_write_value", true, DataSourceUpdateMode.OnPropertyChanged));
            networkUploadValue.DataBindings.Add(new Binding("Text", resources, "network_upload_value", true, DataSourceUpdateMode.OnPropertyChanged));
            networkDownloadValue.DataBindings.Add(new Binding("Text", resources, "network_download_value", true, DataSourceUpdateMode.OnPropertyChanged));
        }
    }
}
