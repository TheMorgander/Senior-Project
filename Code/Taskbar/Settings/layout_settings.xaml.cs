using System.Windows.Controls;

using Config = Taskbar.Config;
using Resources = Taskbar.Resources;
using History = Taskbar.History;

namespace Settings
{
    /// <summary>
    /// Interaction logic for layout_settings.xaml
    /// </summary>
    public partial class layout_settings : Page
    {
        /******************************************************************/
        #region Taskbar Accessors
        protected internal static Config config = Taskbar.Taskbar.config;
        protected internal static History history = Taskbar.Taskbar.history;
        protected internal static Resources resources = Taskbar.Taskbar.resources;
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Watched Variables
        protected internal bool layout_cpu_enabled
        {
            get { return config.GetValue("layout_cpu_enabled"); }
            set { config.SetValue("layout_cpu_enabled", value); }
        }
        protected internal bool layout_gpu_enabled
        {
            get { return config.GetValue("layout_gpu_enabled"); }
            set { config.SetValue("layout_gpu_enabled", value); }
        }
        protected internal bool layout_ram_enabled
        {
            get { return config.GetValue("layout_ram_enabled"); }
            set { config.SetValue("layout_ram_enabled", value); }
        }
        protected internal bool layout_disk_enabled
        {
            get { return config.GetValue("layout_disk_enabled"); }
            set { config.SetValue("layout_disk_enabled", value); }
        }
        protected internal bool layout_network_enabled
        {
            get { return config.GetValue("layout_network_enabled"); }
            set { config.SetValue("layout_network_enabled", value); }
        }
        #endregion
        /******************************************************************/

        /******************************************************************/
        protected internal layout_settings()
        {
            InitializeComponent();
            DataContext = this;
        }
        /******************************************************************/
    }
}
