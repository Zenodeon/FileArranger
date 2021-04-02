using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace FileArranger
{
    static class DebugC
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int FreeConsole();

        //private static Process cmd;

        public static void Initialize()
        {
            AllocConsole();

            Console.WriteLine("testc");
            /*
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            cmd = Process.Start(psi);
            */
            //sr.Close();
        }

        public static void WriteLine(string text)
        {
            //cmd.StandardInput.WriteLine(text);
        }
    }
}
