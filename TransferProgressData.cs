using System;
using System.Collections.Generic;
using System.Text;

namespace FileArranger
{
    public class TransferProgressData
    {
        public ProgressMode mode { get; set; }

        //File
        public int totalFileCount { get; set; }
        public int fileIndex { get; set; }
        public float fileTransferStaticPercentage{ get { return totalFileCount != 0? fileIndex * 100 / totalFileCount : 0; } }

        public float fileTransferPercentage(float dataTransferPercentage)
        {
            return totalFileCount != 0 ? fileIndex * dataTransferPercentage / totalFileCount : 0;
        }

        //Transfer
        public float size { get; set; }

        public float bytesTransfered { get; set; }

        public float dataTransferRate { get; set; }

        public float dataTransferPercentage { get { return bytesTransfered * 100 / size; } }

        public TransferProgressData(ProgressMode mode)
        {
            this.mode = mode;
        }

        public static TransferProgressData CopyFileData(TransferProgressData a, TransferProgressData b)
        {
            a.totalFileCount = b.totalFileCount;
            a.fileIndex = b.fileIndex;

            return a;
        }
    }
    public enum ProgressMode
    {
        dataTransfer,
        fileTransfer
    }
}
