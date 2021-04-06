using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using FileArranger.DebugLogger;

namespace FileArranger
{
    public class DirectoryTree
    {
        public string directoryPath { get; private set; }

        public bool vaild { get; private set; }

        public List<DirectoryTree> subDirectories;

        public string[] files;

        public DirectoryTree(string directoryPath, bool vaild = true)
        {
            this.directoryPath = directoryPath;
            this.vaild = vaild;
        }

        public async void ScanDirectory(bool scanSubDirectories = false)
        {
            await Task.Run(() =>
            {
                subDirectories = GetDirectories();
                files = Directory.GetFiles(directoryPath);
            });

            foreach (DirectoryTree directory in subDirectories)
            {
                DLog.Log(directory.directoryPath);

                directory.ScanDirectory(scanSubDirectories);
            }

            foreach (string file in files)
            {
                DLog.Log(file);

                FileInfo info = new FileInfo(file);

                if (info.Extension != ".json")
                {
                    MediaFile mFile = new MediaFile()
                    {
                        filePath = file,
                        title = info.Name.Split('.')[0]
                    };

                    mFile.SaveCache();
                }
            }
        }

        private List<DirectoryTree> GetDirectories()
        {
            string[] directories = Directory.GetDirectories(directoryPath);

            List<DirectoryTree> directoryList = new List<DirectoryTree>();

            foreach (string directoryPath in directories)
                directoryList.Add(new DirectoryTree(directoryPath)) ;

            return directoryList;
        }
    }
}
