using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace MyWpfInteropControls
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(IMyWpfInteropControlProvider))]
    [Guid("E4D7A364-9646-4E35-9462-3B658618EA03")]
    public class MyWpfInteropControlProvider : IMyWpfInteropControlProvider
    {
        private const int WS_CHILD = 0x40000000;
        private const int WS_TABSTOP = 0x00010000;
        private const int WS_VISIBLE = 0x10000000;

        private HwndSource win32Rect;
        private MyWpfInteropControl wpfControl;

        public void InitializeControl(Int32 region)
        {
            if (region == 0) return;

            IntPtr hwnd = new IntPtr(region);
            win32Rect = new HwndSource(
                0,                      // class style  
                WS_VISIBLE | WS_TABSTOP | WS_CHILD,  // style  
                0,                      // exstyle  
                0, 0, 100, 100,         // Left, Top, Width, Height,  
                "HwndSource",           // Name  
                hwnd                  // Parent window   
            );

            win32Rect.RootVisual = wpfControl = new MyWpfInteropControl();
            
        }

        public void Invalidate()
        {
            wpfControl.InvalidateVisual();
        }

        public void Dispose()
        {
            wpfControl = null;
            win32Rect.Dispose();
            win32Rect = null;
        }
    }
}
