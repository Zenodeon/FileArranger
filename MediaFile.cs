using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;


namespace FileArranger
{
    public class MediaFile : IMediaInfo
    {
        public FileInfo fileInfo { get; set; }
        public string title { get; }
        public string extention { get; }


        private string tempCacheLocation = @"D:\TestSiteDump\";

        public void SaveCache()
        {
            string cache = JsonSerializer.Serialize(this);

            File.WriteAllText(tempCacheLocation + title, cache);
        }
    }
}
