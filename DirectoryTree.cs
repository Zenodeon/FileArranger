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

        ScanProgressData scanInfo = new ScanProgressData();

        public List<DirectoryTree> subDirectories = new List<DirectoryTree>();

        public List<CFile> subFiles = new List<CFile>();
        public List<CFile> selectedSubFiles = new List<CFile>();

        public int totalSubDirectoryCount
        {
            get
            {
                int count = subDirectories.Count;
                foreach (DirectoryTree directory in subDirectories)
                    count += directory.totalSubDirectoryCount;

                return count;
            }
        }

        public int totalSubFileCount
        {
            get
            {
                int count = subFiles.Count;
                foreach (DirectoryTree directory in subDirectories)
                    count += directory.totalSubFileCount;

                return count;
            }
        }

        public int totalSelectedSubFileCount
        {
            get
            {
                int count = selectedSubFiles.Count;
                foreach (DirectoryTree directory in subDirectories)
                    count += directory.totalSelectedSubFileCount;

                return count;
            }
        }

        public DirectoryTree(string directoryPath, bool vaild = true)
        {
            this.directoryPath = directoryPath;
            this.vaild = vaild;
        }

        public async void ScanDirectory(bool scanSubDirectories, IProgress<ScanProgressData> progress = null)
        {
            await ScanDirectory(scanSubDirectories, progress, true);
        }

        public async Task ScanDirectory(bool scanSubDirectories, IProgress<ScanProgressData> progress, bool startDirectory)
        {
            /*
            if (startDirectory)
                CFileInfoCacheHandler.ClearMemoryCache();
            */

            await Task.Run(() =>
            {
                subDirectories = GetDirectories();
                scanInfo.directoriesCount = subDirectories.Count;

                List<string> filesLoc = Directory.GetFiles(directoryPath).ToList();
                scanInfo.fileCount = filesLoc.Count;

                //File Filter [temp]
                selectedSubFiles.Clear();

                foreach (string filePath in filesLoc)
                {
                    CFile file = new CFile(filePath);
                    subFiles.Add(file);

                    if (file.info.extension != ".json")
                    {
                        selectedSubFiles.Add(file);
                        //CFileInfoCacheHandler.AddMediaInfo(file.info.MakeCache());
                    }
                } 
            });

            foreach (DirectoryTree directory in subDirectories)
                await directory.ScanDirectory(scanSubDirectories, progress, false);

            scanInfo.selectedFileCount = selectedSubFiles.Count;

            if (startDirectory)
            {
                //CFileInfoCacheHandler.SaveCache();

                scanInfo.scanDone = true;
            }

            if (progress != null)
                progress.Report(scanInfo);
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
