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
        public CFileInfo info { get; set; }

        public CFile()
        {
        }

        public CFile(string path)
        {
            info = new CFileInfo(new FileInfo(path));
        }

        /*
        public CFile(string cachePath)
        {
            // mediaInfo = new MediaInfo(cachePath);
            mediaInfo = JsonConvert.DeserializeObject<CFileInfo>(cachePath);
        }*/

        public void CopyInfo(CFile file)
        {
            info.CopyInfo(file.info);
        }
    }
}
