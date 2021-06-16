using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileArranger
{
    public static class MediaInfoCacheHandler
    {
        //Temp
        public static string[] tempCacheLocation = { @"D:\TestSiteDump\dump1\", @"D:\TestSiteDump\dump2\", @"D:\TestSiteDump\dump3\" };

        private static List<string> mediaInfoCache = new List<string>();

        public static void AddMediaInfo(JObject mediaInfo)
        {
            mediaInfoCache.Add(mediaInfo.ToString());
        }

        public static void SaveCache(int dump = 0)
        {
            string cache = JsonConvert.SerializeObject(mediaInfoCache);
            File.WriteAllText(tempCacheLocation[dump] + "cache", JObject.Parse(cache).ToString());
        }
    }
}
