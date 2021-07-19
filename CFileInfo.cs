using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using DebugLogger.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;

namespace FileArranger
{
    public class CFileInfo
    {
        private FileInfo fileInfo { get; set; }

        public string filePath { get; set; }
        public string metaDataPath { get; set; }


        public string name { get; set; }
        public string extension { get; set; }


        public string datedCreated { get; set; }

        public CFileInfo(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;

            LoadInfoFromFileInfo(fileInfo);
        }

        public CFileInfo(JObject infoCache)
        {
            //cache = JObject.Parse(File.ReadAllText(cachePath));

            LoadInfoFromCache(infoCache);
        }

        private void LoadInfoFromFileInfo(FileInfo info)
        {
            if (info == null)
                return;

            //FileExtention
            extension = info.Extension;

            //FileName
            name = info.Name.TrimEnd(extension.ToCharArray());

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
            name = infoCache["name"].ToString();

            //FileExtention
            extension = infoCache["extension"].ToString();

            //FilePath
            filePath = infoCache["filePath"].ToString();

            //GooglePhoto's Json File
            metaDataPath = infoCache["metaDataPath"].ToString();
        }

        public JObject MakeCache()
        {
            return JObject.Parse(JsonConvert.SerializeObject(this));
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

        public void CopyInfo(CFileInfo info)
        {
            fileInfo.CreationTimeUtc = info.fileInfo.CreationTimeUtc;
            /*
            using (Stream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                BitmapDecoder decoder = BitmapDecoder.Create(fs, BitmapCreateOptions.None, BitmapCacheOption.Default);
                BitmapFrame frame = decoder.Frames[0]; // the first frame with the metadata
                BitmapMetadata metadata = frame.Metadata as BitmapMetadata;

                if (metadata != null)
                {
                    //DLog.Log(metadata.DateTaken);

                    BitmapMetadata clone = metadata.Clone();

                    clone.DateTaken = new DateTime(2017, 2, 24, 23, 24, 0).ToString("d", CultureInfo.InvariantCulture);

                    metadata = clone;

                    //metadata.DateTaken = metadata.DateTaken;
                    DLog.Log(metadata.DateTaken);
                }

                BitmapEncoder encoder3 = new BitmapEncoder();

                encoder3.Frames.Add(BitmapFrame.Create(decoder.Frames[0],decoder.Frames[0].Thumbnail,metadata,decoder.Frames[0].ColorContexts));

                fs.Close();
            }*/
        }
    }
}
