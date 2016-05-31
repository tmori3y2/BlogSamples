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

namespace CSActiveXUserControl3
{
    [ComVisible(true)]
    [ProgId("CSActiveXUserControl3.UserControl4")]
    [ComDefaultInterface(typeof(IUserControl4))]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("4216699D-7060-464F-A12C-58BC08D65DC5")]
    public partial class UserControl4 : UserControl, IUserControl4
    {
        public UserControl4()
        {
            InitializeComponent();
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
