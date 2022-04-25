using System;
using CSDeskBand;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Taskbar
{
    [ComVisible(true)]
    [Guid("5731FC61-8530-404C-86C1-86CCB8738D06")]
    [CSDeskBandRegistration(Name = "Taskbar Resources", ShowDeskBand = true)]
    public partial class Deskband : CSDeskBandWin
    {
        private static Control _control;

        public Deskband()
        {
            Options.MinHorizontalSize = new Size(400, 40);
            _control = new Taskbar(this);
        }

        protected override Control Control => _control;
    }
}
