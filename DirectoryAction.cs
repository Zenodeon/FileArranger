﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Ookii.Dialogs.Wpf;
using DebugLogger.Wpf;
using System.Threading.Tasks;

namespace FileArranger
{
    static class DirectoryAction
    {
        private static TransferProgressData transferProgressData = new TransferProgressData(ProgressMode.fileTransfer);

        public static DirectoryTree ShowFolderDialog()
        {
            VistaFolderBrowserDialog fileDialog = new VistaFolderBrowserDialog();

            bool choosed;

            if (fileDialog.ShowDialog().Value)
                choosed = true;
            else
                choosed = false;

            return new DirectoryTree(fileDialog.SelectedPath, choosed);
        }

        public static async void TransferContentTo(this DirectoryTree dir, DirectoryTree distinationDir, IProgress<TransferProgressData> progress = null)
        {
            await TransferContentTo(dir, distinationDir, progress, true);
        }

        public static async Task TransferContentTo(this DirectoryTree dir, DirectoryTree distinationDir, IProgress<TransferProgressData> progress, bool startDirectory)
        {
            if (startDirectory)
            {
                transferProgressData = new TransferProgressData(ProgressMode.fileTransfer);
                dir.ScanDirectory(true);
            }
           
            await Task.Run(() =>
            {
                if (startDirectory)
                {
                    transferProgressData.totalFileCount = dir.totalSelectedSubFileCount;
                    transferProgressData.fileIndex = 0;
                }

                List<CFile> file = dir.selectedSubFiles;

                for (int i = 0; i < file.Count; i++)
                {
                    CopyFileTo(file[i], distinationDir.directoryPath, progress);
                    transferProgressData.fileIndex++;
                    progress.Report(transferProgressData);
                }
                /*
                foreach (CFile cFile in dir.subFiles)
                {
                    CopyFileTo(cFile, distinationDir.directoryPath, progress);
                }*/
            });

            foreach (DirectoryTree subDir in dir.subDirectories)
                await TransferContentTo(subDir, distinationDir, progress, false);
        }

        public static CFile CopyFileTo(this CFile file, string distinationPath, IProgress<TransferProgressData> progress)
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            bool cancelFlag = false;
            
            TransferProgressData progressData = new TransferProgressData(ProgressMode.dataTransfer);

            string fileTargetPath = distinationPath + "/" + file.title;

            using (FileStream source = new FileStream(file.info.filePath, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;
                progressData.size = fileLength;

                using (FileStream dest = new FileStream(fileTargetPath, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;

                        progressData.dataTransferRate = currentBlockSize;
                        progressData.bytesTransfered = totalBytes;

                        double persentage = (double)totalBytes * 100.0 / fileLength;

                        dest.Write(buffer, 0, currentBlockSize);

                        cancelFlag = false;
                        //OnProgressChanged(persentage, ref cancelFlag);

                        progress.Report(progressData);

                        if (cancelFlag == true)
                        {
                            // Delete dest file here
                            break;
                        }
                    }
                }
            }

            CFile createdFile = new CFile(fileTargetPath);

            createdFile.CopyInfo(file);

            DLog.Log("Created File : " + createdFile.info.filePath);

            return createdFile;
        }

        public static CFile CopyFileTo(this CFile file, DirectoryTree distinationDir, IProgress<TransferProgressData> progress)
        {
            return CopyFileTo(file, distinationDir.directoryPath, progress);
        }

        public static void MoveContentTo(DirectoryTree distinationDir)
        {

        }
    }
}
