using System.Windows.Controls;

using Config = Taskbar.Config;
using Resources = Taskbar.Resources;
using History = Taskbar.History;

namespace Settings
{
    /// <summary>
    /// Interaction logic for history_settings.xaml
    /// </summary>
    public partial class history_settings : Page
    {
        /******************************************************************/
        #region Taskbar Accessors
        protected internal static Config config = Taskbar.Taskbar.config;
        protected internal static History history = Taskbar.Taskbar.history;
        protected internal static Resources resources = Taskbar.Taskbar.resources;
        #endregion
        /******************************************************************/

        protected internal history_settings()
        {
            InitializeComponent();
        }
    }
}
