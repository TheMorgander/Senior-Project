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
        protected internal static Config config = Taskbar.Taskbar.config;
        protected internal static History history = Taskbar.Taskbar.history;
        protected internal static Resources resources = Taskbar.Taskbar.resources;
        #endregion
        /******************************************************************/

        protected internal MainWindow()
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
