using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileArranger
{
    public class MediaInfo
    {
        private FileInfo fileInfo { get; set; }
        private JObject cache { get; set; }

        public string filePath { get; set; }
        public string metaDataPath { get; set; }


        public string title { get; set; }
        public string extention { get; set; }


        public string datedCreated { get; set; }


        //Temp
        public static string[] tempCacheLocation = { @"D:\TestSiteDump\dump1\", @"D:\TestSiteDump\dump2\", @"D:\TestSiteDump\dump3\"};

        public MediaInfo(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;

            LoadInfoFromFileInfo();
        }

        public MediaInfo(string cachePath)
        {
            cache = JObject.Parse(File.ReadAllText(cachePath));

            LoadInfoFromCache();
        }

        private void LoadInfoFromFileInfo()
        {
            if (fileInfo == null)
                return;

            //FileName
            title = fileInfo.Name.Split('.')[0];

            //FileExtention
            extention = fileInfo.Extension.Split('.')[1];

            //FilePath
            filePath = fileInfo.FullName;

            //GooglePhoto's Json File
            metaDataPath = GetMediaMetaDataPath();

            datedCreated = "";
        }
        private void LoadInfoFromCache()
        {
            if (cache == null)
                return;

            //FileName
            title = cache["title"].ToString();

            //FileExtention
            extention = cache["extention"].ToString();

            //FilePath
            filePath = cache["filePath"].ToString();

            //GooglePhoto's Json File
            metaDataPath = cache["metaDataPath"].ToString();
        }

        public void SaveCache(int dump = 0)
        {
            JObject cache = JObject.Parse(JsonConvert.SerializeObject(this));

            File.WriteAllText(tempCacheLocation[dump] + title, cache.ToString());
        }

        //GooglePhoto's Json File
        private string GetMediaMetaDataPath()
        {
            string jsonPath1 = fileInfo.FullName + ".json";
            string jsonPath2 = fileInfo.FullName.Split('.')[0] + ".json";
            string returnPath = "";

            if (File.Exists(jsonPath1))
                returnPath = jsonPath1;
            else if (File.Exists(jsonPath2))
                returnPath = jsonPath2;

            return returnPath;
        }

    }
}
