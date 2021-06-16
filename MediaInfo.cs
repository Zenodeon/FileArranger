﻿using System;
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

        public string filePath { get; set; }
        public string metaDataPath { get; set; }


        public string title { get; set; }
        public string extention { get; set; }


        public string datedCreated { get; set; }

        public MediaInfo(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;

            LoadInfoFromFileInfo(fileInfo);
        }

        public MediaInfo(JObject infoCache)
        {
            //cache = JObject.Parse(File.ReadAllText(cachePath));

            LoadInfoFromCache(infoCache);
        }

        private void LoadInfoFromFileInfo(FileInfo info)
        {
            if (info == null)
                return;

            //FileName
            title = info.Name.Split('.')[0];

            //FileExtention
            extention = info.Extension.Split('.')[1];

            //FilePath
            filePath = info.FullName;

            //GooglePhoto's Json File
            metaDataPath = GetMediaMetaDataPath(info);

            datedCreated = "";
        }
        private void LoadInfoFromCache(JObject infoCache)
        {
            if (infoCache == null)
                return;

            //FileName
            title = infoCache["title"].ToString();

            //FileExtention
            extention = infoCache["extention"].ToString();

            //FilePath
            filePath = infoCache["filePath"].ToString();

            //GooglePhoto's Json File
            metaDataPath = infoCache["metaDataPath"].ToString();
        }

        public void MakeCache(int dump = 0)
        {
            JObject info = JObject.Parse(JsonConvert.SerializeObject(this));

            MediaInfoCacheHandler.AddMediaInfo(info);
        }

        //GooglePhoto's Json File
        private string GetMediaMetaDataPath(FileInfo info)
        {
            string jsonPath1 = info.FullName + ".json";
            string jsonPath2 = info.FullName.Split('.')[0] + ".json";
            string returnPath = "";

            if (File.Exists(jsonPath1))
                returnPath = jsonPath1;
            else if (File.Exists(jsonPath2))
                returnPath = jsonPath2;

            return returnPath;
        }
    }
}
