using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CSActiveXUserControl2
{
    internal static class DispathId
    {
        public const int DISPID_REFRESH = unchecked((int)0xFFFFFDDA);
    }

    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("B4D237D0-07CE-4867-9282-0CDCB5D11D8B")]
    public interface IUserControl2
    {
        [DispId(DispathId.DISPID_REFRESH)]
        void Refresh();
    }
}
