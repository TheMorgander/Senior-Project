using System.Windows;
using System.Windows.Navigation;

using Config = Taskbar.Config;
using Resources = Taskbar.Resources;
using History = Taskbar.History;

namespace Settings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /******************************************************************/
        #region Taskbar Accessors
        public static Config config = Taskbar.Taskbar.taskbar.config;
        public static History history = Taskbar.Taskbar.taskbar.history;
        public static Resources resources = Taskbar.Taskbar.taskbar.resources;
        #endregion
        /******************************************************************/

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new general_settings();
            Main.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        private void general_settings_click(object sender, RoutedEventArgs e)
        {
            Main.Content = new general_settings();
        }

        private void layout_settings_click(object sender, RoutedEventArgs e)
        {
            Main.Content = new layout_settings();
        }

        private void profile_settings_click(object sender, RoutedEventArgs e)
        {
            Main.Content = new profile_settings();
        }

        private void history_settings_click(object sender, RoutedEventArgs e)
        {
            Main.Content = new history_settings();
        }

        private void about_click(object sender, RoutedEventArgs e)
        {
            Main.Content = new about();
        }
    }
}
