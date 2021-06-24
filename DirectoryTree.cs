using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DebugLogger.Wpf;

namespace FileArranger
{
    public class DirectoryTree
    {
        public string directoryPath { get; private set; }

        public bool vaild { get; private set; }

        public List<DirectoryTree> subDirectories = new List<DirectoryTree>();

        public List<CFile> subFiles = new List<CFile>();

        public DirectoryTree(string directoryPath, bool vaild = true)
        {
            this.directoryPath = directoryPath;
            this.vaild = vaild;
        }

        public async Task ScanDirectory(bool scanSubDirectories, IProgress<ScanProgressData> progress, bool startDirectory = false)
        {
            ScanProgressData scanData = new ScanProgressData();

            if (startDirectory)
                CFileInfoCacheHandler.ClearMemoryCache();

            await Task.Run(async () =>
            {
                subDirectories = GetDirectories();
                scanData.directoriesCount += subDirectories.Count;

                foreach (DirectoryTree directory in subDirectories)
                    await directory.ScanDirectory(scanSubDirectories, progress);

                List<string> filesLoc = Directory.GetFiles(directoryPath).ToList();
                scanData.fileCount += filesLoc.Count;

                foreach (string file in filesLoc)
                {
                    FileInfo info = new FileInfo(file);

                    if (info.Extension != ".json")
                    {
                        CFile mediaFile = new CFile(info);
                        subFiles.Add(mediaFile);
                        CFileInfoCacheHandler.AddMediaInfo(mediaFile.mediaInfo.MakeCache());
                    }
                }
              
                progress.Report(scanData);
            });

            if (startDirectory)
            {
                CFileInfoCacheHandler.SaveCache();

                scanData.scanDone = true;
                progress.Report(scanData);
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
