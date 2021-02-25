using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DemoLibrary
{
    public class MyDictionary
    {
        private IntPtr pairs;

        public int Size { get => GetSize(pairs); }

        public int Length { get => GetLength(pairs); }

        public int Memory { get => Marshal.SizeOf(typeof(KeyValuePair)) * (Size + 1); }

        public MyDictionary()
        {
            pairs = Init();
        }

        public void TearDown()
        {
            Marshal.FreeHGlobal(pairs);
        }

        public int this[string key]
        {
            get
            {
                byte[] chars = Encoding.UTF8.GetBytes(key);
                return Get(pairs, chars);
            }
        }

        public void Put(string key, int value)
        {
            byte[] chars = Encoding.UTF8.GetBytes(key);
            pairs = Put(pairs, chars, value);
        }

        [DllImport("libdict.so")] // the library has to be inside the folder in which the project is executed
        private static extern IntPtr Init();

        [DllImport("libdict.so")]
        private static extern IntPtr Put(IntPtr pairs, byte[] key, int value);

        [DllImport("libdict.so")]
        private static extern int Get(IntPtr pairs, byte[] key);

        [DllImport("libdict.so")]
        private static extern int GetSize(IntPtr pairs);

        [DllImport("libdict.so")]
        private static extern int GetLength(IntPtr pairs);
    }
}
