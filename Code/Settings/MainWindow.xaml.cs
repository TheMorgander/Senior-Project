using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Settings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Taskbar.Settings.Initalize();
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
