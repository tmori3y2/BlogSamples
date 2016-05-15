using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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
        private UserControl1 wpfControl;

        public UserControl5()
        {
            InitializeComponent();

            wpfHost = new ElementHost();
            wpfHost.Child = wpfControl = new UserControl1();
            wpfHost.Dock = DockStyle.Fill;
            wpfHost.TabStop = true;
            Controls.Add(wpfHost);

            OnCreateControl();
        }
    }
}
