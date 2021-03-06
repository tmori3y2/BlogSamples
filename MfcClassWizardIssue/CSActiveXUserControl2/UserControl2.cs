﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace CSActiveXUserControl2
{
    [ComVisible(true)]
    [ProgId("CSActiveXUserControl2.UserControl2")]
    [ComDefaultInterface(typeof(IUserControl2))]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("F8335586-7635-4249-8ED8-BD450A5057E9")]
    public partial class UserControl2 : UserControl, IUserControl2
    {
        public UserControl2()
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

        #region IUserControl2 Members

        void IUserControl2.Refresh()
        {
            Refresh();
        }

        #endregion
    }
}
