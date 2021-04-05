using System;
using System.Collections.Generic;
using System.Text;

namespace FileArranger.DebugLogger
{
    static class DLog
    {
        private static LoggerWindow logWindow;

        public static void Instantiate()
        {
            logWindow = new LoggerWindow();
            logWindow.Show();
        }

        public static void Close()
        {
            logWindow.Close();
        }

        public static void Log(string log)
        {
            logWindow.NewLog(log);
        }
    }
}
