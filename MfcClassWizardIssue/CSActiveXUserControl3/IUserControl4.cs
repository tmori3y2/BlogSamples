using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSActiveXUserControl3
{
    internal static class DispathId
    {
        public const int DISPID_REFRESH = unchecked((int)0xFFFFFDDA);
    }

    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("C6888420-A901-4C76-BA0F-EE3B1ED1D303")]
    [CoClass(typeof(UserControl4))]
    public interface IUserControl4
    {
        [DispId(DispathId.DISPID_REFRESH)]
        void Refresh();
    }
}
