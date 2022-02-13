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
        }
    }
}
