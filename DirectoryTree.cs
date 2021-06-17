using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DebugLogger.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public async void ScanDirectory(bool scanSubDirectories, IProgress<ScanProgressData> progress, MediaInfoCacheHandler cacheHandler, bool startDirectory = false)
        {
            ScanProgressData scanData = new ScanProgressData();

            await Task.Run(() =>
            {
                files = Directory.GetFiles(directoryPath).ToList();
                scanData.fileCount += files.Count;

                subDirectories = GetDirectories();
                scanData.directoriesCount += subDirectories.Count;

                foreach (DirectoryTree directory in subDirectories)
                    directory.ScanDirectory(scanSubDirectories, progress, cacheHandler);

                foreach (string file in files)
                {
                    FileInfo info = new FileInfo(file);

                    if (info.Extension != ".json")
                        cacheHandler.AddMediaInfo(new MediaFile(info).mediaInfo.MakeCache());
                }

                progress.Report(scanData);
            });

            if (startDirectory)
            {
                cacheHandler.SaveCache();
                //scanData.scanDone = true;
                //progress.Report(scanData);
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
