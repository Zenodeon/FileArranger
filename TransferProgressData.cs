using System;
using System.Collections.Generic;
using System.Text;

namespace FileArranger
{
    public class TransferProgressData
    {
        public IProgress<TransferProgressData> receiver { private get; set; }

        //File
        public int totalFileCount { get; set; }
        public int fileIndex { get; set; }
        public float fileTransferStaticPercentage { get { return totalFileCount != 0 ? ++fileIndex * 100 / totalFileCount : 0; } }

        public float fileTransferPercentage { get { return totalFileCount != 0 ? fileIndex * dataTransferPercentage / totalFileCount : 0; } }

        //Transfer
        public float size { get; set; }

        public float bytesTransfered { get; set; }

        public float dataTransferRate { get; set; }

        public float dataTransferPercentage { get { return bytesTransfered * 100 / size; } }

        public TransferProgressData(IProgress<TransferProgressData> receiver)
        {
            this.receiver = receiver;
        }

        public void Report()
        {
            receiver.Report(this);
        }
    }
    public enum ProgressMode
    {
        dataTransfer,
        fileTransfer
    }
}
