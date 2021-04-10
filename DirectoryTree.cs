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

        public List<string> files;

        public DirectoryTree(string directoryPath, bool vaild = true)
        {
            this.directoryPath = directoryPath;
            this.vaild = vaild;
        }

        public async void ScanDirectory(bool scanSubDirectories, IProgress<ScanProgressData> progress)
        {
            await Task.Run(() =>
            {
                ScanProgressData scanData = new ScanProgressData();

                files = Directory.GetFiles(directoryPath).ToList();
                scanData.fileCount += files.Count;

                subDirectories = GetDirectories();
                scanData.directoriesCount += subDirectories.Count;

                foreach (DirectoryTree directory in subDirectories)
                    directory.ScanDirectory(scanSubDirectories, progress);

                foreach (string file in files)
                {
                    FileInfo info = new FileInfo(file);

                    if (info.Extension != ".json")
                        new MediaFile(info).SaveCache(); ;
                }

                progress.Report(scanData);
            });
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
