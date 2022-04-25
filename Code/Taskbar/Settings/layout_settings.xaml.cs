using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Settings
{
    /// <summary>
    /// Interaction logic for layout_settings.xaml
    /// </summary>
    public partial class layout_settings : Page
    {
        /******************************************************************/
        #region Watched Variables
        public bool layout_cpu_enabled
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_cpu_enabled")); }
            set { Taskbar.Config.SetValue("layout_cpu_enabled", value.ToString()); OnPropertyChanged("layout_cpu_enabled"); }
        }
        public bool layout_gpu_enabled
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_gpu_enabled")); }
            set { Taskbar.Config.SetValue("layout_gpu_enabled", value.ToString()); OnPropertyChanged("layout_gpu_enabled"); }
        }
        public bool layout_ram_enabled
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_ram_enabled")); }
            set { Taskbar.Config.SetValue("layout_ram_enabled", value.ToString()); OnPropertyChanged("layout_ram_enabled"); }
        }
        public bool layout_disk_enabled
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_disk_enabled")); }
            set { Taskbar.Config.SetValue("layout_disk_enabled", value.ToString()); OnPropertyChanged("layout_disk_enabled"); }
        }
        public bool layout_network_enabled
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_network_enabled")); }
            set { Taskbar.Config.SetValue("layout_network_enabled", value.ToString()); OnPropertyChanged("layout_network_enabled"); }
        }

        public bool layout_header_enabled
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_header_enabled")); }
            set { Taskbar.Config.SetValue("layout_header_enabled", value.ToString()); OnPropertyChanged("layout_header_enabled"); }
        }
        public bool layout_header_bold
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_header_bold")); }
            set { Taskbar.Config.SetValue("layout_header_bold", value.ToString()); OnPropertyChanged("layout_header_bold"); }
        }
        public bool layout_header_italic
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_header_italic")); }
            set { Taskbar.Config.SetValue("layout_header_italic", value.ToString()); OnPropertyChanged("layout_header_italic"); }
        }
        public bool layout_header_underline
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_header_underline")); }
            set { Taskbar.Config.SetValue("layout_header_underline", value.ToString()); OnPropertyChanged("layout_header_underline"); }
        }

        public string layout_cpu_color
        {
            get { return Taskbar.Config.GetValue("layout_cpu_color"); }
            set { Taskbar.Config.SetValue("layout_cpu_color", value.ToString()); OnPropertyChanged("layout_cpu_color"); }
        }
        public bool layout_cpu_bold
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_cpu_bold")); }
            set { Taskbar.Config.SetValue("layout_cpu_bold", value.ToString()); OnPropertyChanged("layout_cpu_bold"); }
        }
        public bool layout_cpu_italic
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_cpu_italic")); }
            set { Taskbar.Config.SetValue("layout_cpu_italic", value.ToString()); OnPropertyChanged("layout_cpu_italic"); }
        }
        public bool layout_cpu_underline
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_cpu_underline")); }
            set { Taskbar.Config.SetValue("layout_cpu_underline", value.ToString()); OnPropertyChanged("layout_cpu_underline"); }
        }

        public string layout_gpu_color
        {
            get { return Taskbar.Config.GetValue("layout_gpu_color"); }
            set { Taskbar.Config.SetValue("layout_gpu_color", value.ToString()); OnPropertyChanged("layout_gpu_color"); }
        }
        public bool layout_gpu_bold
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_gpu_bold")); }
            set { Taskbar.Config.SetValue("layout_gpu_bold", value.ToString()); OnPropertyChanged("layout_gpu_bold"); }
        }
        public bool layout_gpu_italic
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_gpu_italic")); }
            set { Taskbar.Config.SetValue("layout_gpu_italic", value.ToString()); OnPropertyChanged("layout_gpu_italic"); }
        }
        public bool layout_gpu_underline
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_gpu_underline")); }
            set { Taskbar.Config.SetValue("layout_gpu_underline", value.ToString()); OnPropertyChanged("layout_gpu_underline"); }
        }

        public string layout_ram_color
        {
            get { return Taskbar.Config.GetValue("layout_ram_color"); }
            set { Taskbar.Config.SetValue("layout_ram_color", value.ToString()); OnPropertyChanged("layout_ram_color"); }
        }
        public bool layout_ram_bold
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_ram_bold")); }
            set { Taskbar.Config.SetValue("layout_ram_bold", value.ToString()); OnPropertyChanged("layout_ram_bold"); }
        }
        public bool layout_ram_italic
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_ram_italic")); }
            set { Taskbar.Config.SetValue("layout_ram_italic", value.ToString()); OnPropertyChanged("layout_ram_italic"); }
        }
        public bool layout_ram_underline
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_ram_underline")); }
            set { Taskbar.Config.SetValue("layout_ram_underline", value.ToString()); OnPropertyChanged("layout_ram_underline"); }
        }

        public string layout_disk_color
        {
            get { return Taskbar.Config.GetValue("layout_disk_color"); }
            set { Taskbar.Config.SetValue("layout_disk_color", value.ToString()); OnPropertyChanged("layout_disk_color"); }
        }
        public bool layout_disk_bold
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_disk_bold")); }
            set { Taskbar.Config.SetValue("layout_disk_bold", value.ToString()); OnPropertyChanged("layout_disk_bold"); }
        }
        public bool layout_disk_italic
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_disk_italic")); }
            set { Taskbar.Config.SetValue("layout_disk_italic", value.ToString()); OnPropertyChanged("layout_disk_italic"); }
        }
        public bool layout_disk_underline
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_disk_underline")); }
            set { Taskbar.Config.SetValue("layout_disk_underline", value.ToString()); OnPropertyChanged("layout_disk_underline"); }
        }

        public string layout_network_color
        {
            get { return Taskbar.Config.GetValue("layout_network_color"); }
            set { Taskbar.Config.SetValue("layout_network_color", value.ToString()); OnPropertyChanged("layout_network_color"); }
        }
        public bool layout_network_bold
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_network_bold")); }
            set { Taskbar.Config.SetValue("layout_network_bold", value.ToString()); OnPropertyChanged("layout_network_bold"); }
        }
        public bool layout_network_italic
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_network_italic")); }
            set { Taskbar.Config.SetValue("layout_network_italic", value.ToString()); OnPropertyChanged("layout_network_italic"); }
        }
        public bool layout_network_underline
        {
            get { return bool.Parse(Taskbar.Config.GetValue("layout_network_underline")); }
            set { Taskbar.Config.SetValue("layout_network_underline", value.ToString()); OnPropertyChanged("layout_network_underline"); }
        }
        #endregion
        /******************************************************************/

        /******************************************************************/
        public layout_settings()
        {
            InitializeComponent();
            DataContext = this;
        }
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
    }
}
