using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        public static Config config = Taskbar.Taskbar.taskbar.config;
        public static History history = Taskbar.Taskbar.taskbar.history;
        public static Resources resources = Taskbar.Taskbar.taskbar.resources;
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Watched Variables
        public bool layout_cpu_enabled
        {
            get { return config.layout_cpu_enabled; }
            set { config.layout_cpu_enabled = value; OnPropertyChanged("layout_cpu_enabled"); }
        }
        public bool layout_gpu_enabled
        {
            get { return Taskbar.Taskbar.taskbar.config.layout_gpu_enabled; ; }
            set { config.layout_gpu_enabled = value; ; OnPropertyChanged("layout_gpu_enabled"); }
        }
        public bool layout_ram_enabled
        {
            get { return config.layout_ram_enabled; }
            set { config.layout_ram_enabled = value; OnPropertyChanged("layout_ram_enabled"); }
        }
        public bool layout_disk_enabled
        {
            get { return config.layout_disk_enabled; }
            set { config.layout_disk_enabled = value; OnPropertyChanged("layout_disk_enabled"); }
        }
        public bool layout_network_enabled
        {
            get { return config.layout_network_enabled; }
            set { config.layout_network_enabled = value; OnPropertyChanged("layout_network_enabled"); }
        }
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Variable Listener
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        /******************************************************************/

        /******************************************************************/
        public layout_settings()
        {
            InitializeComponent();
            DataContext = this;
        }
        /******************************************************************/
    }
}
