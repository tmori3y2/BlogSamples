using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

using WpfControl1;

namespace CSActiveXUserControl3
{
    [ComVisible(true)]
    [ProgId("CSActiveXUserControl3.UserControl5")]
    [Guid("A43D9C25-4C20-4323-B744-3A6172DB7D43")]
    public partial class UserControl5 : UserControl
    {
        private ElementHost wpfHost;
        private WpfUserControl1 wpfControl;

        public UserControl5()
        {
            InitializeComponent();

            wpfHost = new ElementHost();
            wpfHost.Child = wpfControl = new WpfUserControl1();
            wpfHost.Dock = DockStyle.Fill;
            Controls.Add(wpfHost);

            OnCreateControl();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_SETFOCUS = 0x7;
            const int WM_KILLFOCUS = 0x8;
            const int WM_CLOSE = 0x10;
            const int WM_SIZE = 0x5;
            const int WM_PARENTNOTIFY = 0x210;
            const int WM_DESTROY = 0x2;
            const int WM_LBUTTONDOWN = 0x201;
            const int WM_RBUTTONDOWN = 0x204;

            if (m.Msg == WM_SETFOCUS)
            {
                // Raise Enter event
                this.OnEnter(System.EventArgs.Empty);
            }
            else if (m.Msg == WM_PARENTNOTIFY && (
                m.WParam.ToInt32() == WM_LBUTTONDOWN ||
                m.WParam.ToInt32() == WM_RBUTTONDOWN))
            {
                if (!this.ContainsFocus)
                {
                    // Raise Enter event
                    this.OnEnter(System.EventArgs.Empty);
                }
            }
            else if (m.Msg == WM_KILLFOCUS ||
                     m.Msg == WM_CLOSE)
            {
                // Raise Leave event
                this.OnLeave(System.EventArgs.Empty);
            }
            else if (m.Msg == WM_SIZE)
            {
                this.Refresh();
            }
            else if (m.Msg == WM_DESTROY &&
                !this.IsDisposed && !this.Disposing)
            {
                // Used to ensure the cleanup of the control
                this.Dispose();
            }

            base.WndProc(ref m);
        }

    }
}
