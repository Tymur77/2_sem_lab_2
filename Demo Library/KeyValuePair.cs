using System;
using System.Runtime.InteropServices;

namespace DemoLibrary
{
    [StructLayout(LayoutKind.Sequential)]
    struct KeyValuePair
    {
        public int value;
        public IntPtr key;
    }
}
