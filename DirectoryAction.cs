using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Ookii.Dialogs.Wpf;
using DebugLogger.Wpf;
namespace FileArranger
{
    static class DirectoryAction
    {
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

        public static CFile CopyFileTo(this CFile file, DirectoryTree distinationDir)
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            bool cancelFlag = false;

            string fileTargetPath = distinationDir.directoryPath + "/" + file.info.title + "." + file.info.extension;

            using (FileStream source = new FileStream(file.info.filePath, FileMode.Open, FileAccess.Read))
            {              
                long fileLength = source.Length;
                using (FileStream dest = new FileStream(fileTargetPath, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;

                        dest.Write(buffer, 0, currentBlockSize);

                        cancelFlag = false;
                        //OnProgressChanged(persentage, ref cancelFlag);

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

        public static void MoveContentTo(DirectoryTree distinationDir)
        {

        }
    }
}
