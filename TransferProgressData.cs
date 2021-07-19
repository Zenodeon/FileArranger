using System;
using System.Collections.Generic;
using System.Text;

namespace FileArranger
{
    public class TransferProgressData
    {
        public ProgressMode mode { get; set; }

        //File
        public int totalFile { get; set; }
        public int fileIndex { get; set; }
        public float filePercentage { get { return totalFile != 0? fileIndex * 100 / totalFile : 0; } }

        //Transfer
        public float size { get; set; }

        public float transferedBytes { get; set; }

        public float transferRate { get; set; }

        public float transferPercentage { get { return transferedBytes * 100 / size; } }

        public TransferProgressData(ProgressMode mode)
        {
            this.mode = mode;
        }
    }
    public enum ProgressMode
    {
        transfer,
        file
    }
}
