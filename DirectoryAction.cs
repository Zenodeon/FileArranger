using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Ookii.Dialogs.Wpf;

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

        public static void CopyFileTo(this MediaFile file, DirectoryTree distinationDir)
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            bool cancelFlag = false;

            using (FileStream source = new FileStream(file.mediaInfo.filePath, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;
                using (FileStream dest = new FileStream(distinationDir.directoryPath + "/" + file.mediaInfo.title + "." + file.mediaInfo.extention, FileMode.CreateNew, FileAccess.Write))
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
        }

        public static void MoveContentTo(DirectoryTree distinationDir)
        {

        }
    }
}
