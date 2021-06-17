using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileArranger
{
    public class ScanProgressData
    {
        public int directoriesCount { get; set; } = 0;
        public int fileCount { get; set; } = 0;

        public bool scanDone { get; set; } = false;

        public static ScanProgressData operator+(ScanProgressData a, ScanProgressData b)
        {
            a.directoriesCount += b.directoriesCount;
            a.fileCount += b.fileCount;

            return a;
        }
    }
}
