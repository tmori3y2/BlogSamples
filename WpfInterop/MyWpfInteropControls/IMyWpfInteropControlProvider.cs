using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfInteropControls
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("84ED9BE4-5DBF-489C-B982-DDEF98F6EB61")]
    public interface IMyWpfInteropControlProvider
    {
        [DispId(0x0100)]
        void InitializeControl(Int32 region);

        [DispId(0x0101)]
        void Dispose();

        [DispId(0x0102)]
        void Invalidate();
    }
}
