using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DebugLogger.Wpf;

namespace FileArranger
{
    public static class CFileInfoCacheHandler
    {
        //Temp
        public static string[] tempCacheLocation = { @"D:\TestSiteDump\dump1\", @"D:\TestSiteDump\dump2\", @"D:\TestSiteDump\dump3\" };

        private static JArray mediaInfoCache = new JArray();

        public static void AddMediaInfo(JObject mediaInfo)
        {   
            //mediaInfoCache.Add(mediaInfo);
        }

        public static void ClearMemoryCache()
        {
            //mediaInfoCache.Clear();
        }

        public static void SaveCache()
        {
            /*
            DLog.Log("saving count : " + mediaInfoCache.Count);

            File.WriteAllText(tempCacheLocation[0] + "cache", mediaInfoCache.ToString());
            */
        }
    }
}
