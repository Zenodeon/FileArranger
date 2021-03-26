using System;
using System.Collections.Generic;
using System.Text;

namespace FileArranger
{
    public class DirectoryInventory
    {
        public string path { get; private set; }

        public DirectoryInventory(string directoryPath)
        {
            path = directoryPath;
        }
    }
}
