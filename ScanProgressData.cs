using System;
using System.Collections.Generic;
using System.Text;

namespace FileArranger
{
    public class ScanProgressData
    {
        public int directoriesCount { get; set; } = 0;
        public int fileCount { get; set; } = 0;

        public static ScanProgressData operator+(ScanProgressData a, ScanProgressData b)
        {
            a.directoriesCount += b.directoriesCount;
            a.fileCount += b.fileCount;

            return a;
        }


    }
}
