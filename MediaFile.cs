using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FileArranger
{
    public class MediaFile
    {
        public MediaInfo mediaInfo { get; set; }

        public MediaFile()
        {
        }

        public MediaFile(FileInfo info)
        {
            mediaInfo = new MediaInfo(info);
        }

        public MediaFile(string cachePath)
        {
            // mediaInfo = new MediaInfo(cachePath);
            mediaInfo = JsonConvert.DeserializeObject<MediaInfo>(cachePath);
        }
    }
}
