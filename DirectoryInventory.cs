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

        public string[] files;

        public DirectoryInventory(string directoryPath, bool vaildDirectory)
        {
            path = directoryPath;
            vaild = vaildDirectory & Directory.Exists(directoryPath);
        }

        public string[] ScanDirectory()
        {
            files = Directory.GetFiles(path);

            foreach(string path in files)
            {
                //Debug.WriteLine(path);
                DebugC.WriteLine(path);
            }

            return files;
        }
    }
}
