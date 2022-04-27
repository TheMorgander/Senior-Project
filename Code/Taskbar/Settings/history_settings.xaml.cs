﻿using System.Windows.Controls;

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
        public static Config config = Taskbar.Taskbar.taskbar.config;
        public static History history = Taskbar.Taskbar.taskbar.history;
        public static Resources resources = Taskbar.Taskbar.taskbar.resources;
        #endregion
        /******************************************************************/

        public history_settings()
        {
            InitializeComponent();
        }
    }
}
