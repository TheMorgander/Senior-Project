using System;
using CSDeskBand;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Taskbar
{
    [ComVisible(true)]
    [Guid("5731FC61-8530-404C-86C1-86CCB8738D06")]
    [CSDeskBandRegistration(Name = "Taskbar Resources")]
    public partial class Deskband : CSDeskBandWin
    {
        public Deskband()
        {
            Options.MinHorizontalSize = new Size(100, 30);
        }

        protected override Control Control => new Taskbar();
    }
}
