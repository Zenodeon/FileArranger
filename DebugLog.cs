using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FileArranger
{
    static class DebugLog
    {
#if DEBUG
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();
#endif
        public static void Instantiate()
        {
#if DEBUG
            AllocConsole();
#endif
        }

        public static void Write(string text)
        {
#if DEBUG
            Console.WriteLine(text);
#endif           
        }
    }
}
