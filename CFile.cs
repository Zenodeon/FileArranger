using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FileArranger
{
    public class CFile
    {
        public CFileInfo mediaInfo { get; set; }

        public CFile()
        {
        }

        public CFile(FileInfo info)
        {
            mediaInfo = new CFileInfo(info);
        }

        public CFile(string cachePath)
        {
            // mediaInfo = new MediaInfo(cachePath);
            mediaInfo = JsonConvert.DeserializeObject<CFileInfo>(cachePath);
        }
    }
}
