using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FileArranger
{
    public class DirectoryInventory
    {
        public string path { get; private set; }

        public bool vaild { get; private set; }

        public string[] folders;
        public string[] files;

        public DirectoryInventory(string directoryPath, bool vaild)
        {
            path = directoryPath;
            this.vaild = vaild;
        }

        public string[] ScanDirectory()
        {
            folders = Directory.GetDirectories(path);
            files = Directory.GetFiles(path);

            foreach (string folder in folders)
            {
                DebugLog.Write(folder);
            }

            foreach (string file in files)
            {
                DebugLog.Write(file);
            }

            return files;
        }
    }
}
