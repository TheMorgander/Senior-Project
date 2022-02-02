﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CSDeskBand;
using CSDeskBand.ContextMenu;

namespace Taskbar
{
    [ComVisible(true)]
    [CSDeskBandRegistration(Name = "Taskbar Resources")]
    public class Deskband : CSDeskBandWpf
    {
        public Deskband()
        {
            Options.ContextMenuItems = ContextMenuItems;
            Options.MinHorizontalSize = new DeskBandSize(50, -1);
        }

        protected override UIElement UIElement => new Taskbar();

        private List<DeskBandMenuItem> ContextMenuItems
        {
            get
            {
                var action = new DeskBandMenuAction("Action - Toggle submenu");
                var separator = new DeskBandMenuSeparator();
                var submenuAction = new DeskBandMenuAction("Submenu Action - Toggle checkmark");
                var submenu = new DeskBandMenu("Submenu")
                {
                    Items = { submenuAction }
                };

                action.Clicked += (sender, args) => submenu.Enabled = !submenu.Enabled;
                submenuAction.Clicked += (sender, args) => submenuAction.Checked = !submenuAction.Checked;

                return new List<DeskBandMenuItem>() { action, separator, submenu };
            }
        }
    }
}