using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;


namespace FileArranger
{
    public class MediaFile
    {
        private FileInfo fileInfo { get; set; }

        public string title { get { return fileInfo.Name.Split('.')[0]; } }
        public string extention { get { return fileInfo.Extension.Split('.')[1]; } }

        public string metaDataPath 
        { get 
            {
                string jsonPath1 = fileInfo.FullName + ".json";
                string jsonPath2 = fileInfo.FullName.Split('.')[0] + ".json";
                string returnPath = "";

                if (File.Exists(jsonPath1))
                    returnPath = jsonPath1;
                else if(File.Exists(jsonPath2))
                    returnPath = jsonPath2;
 
                return returnPath;
            } 
        }

        public string datedCreated
        {
            get
            {
                if (metaDataPath == "")
                    return "";

                 JsonSerializer.Serialize(File.ReadAllText(metaDataPath));

                return "";
            }
        }

        private string tempCacheLocation = @"D:\TestSiteDump\";

        public MediaFile(FileInfo info)
        {
            fileInfo = info;
        }

        public void SaveCache()
        {
            string cache = JsonSerializer.Serialize(this);

            File.WriteAllText(tempCacheLocation + title, cache);
        }
    }
}
