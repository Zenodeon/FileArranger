using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DebugLogger.Wpf;

namespace FileArranger
{
    public class MediaInfoCacheHandler
    {
        //Temp
        public static string[] tempCacheLocation = { @"D:\TestSiteDump\dump1\", @"D:\TestSiteDump\dump2\", @"D:\TestSiteDump\dump3\" };

        private List<JObject> mediaInfoCache = new List<JObject>();

        private JArray test = new JArray();

        public void AddMediaInfo(JObject mediaInfo)
        {
            //mediaInfoCache.Add(mediaInfo);

            test.Add(mediaInfo);
            DLog.Log("file+1");
            //File.WriteAllText(tempCacheLocation[1] + "cache" + test.Count, mediaInfo.ToString());
        }

        public void AddMediaInfo(JArray mediaInfo)
        {
            //mediaInfoCache.Add(mediaInfo);

            //test.Add(mediaInfo);

            //foreach (JToken j in mediaInfo)
          //  {
               // test.Add(j);
              //  DLog.Log("file+1");
            //}

            DLog.Log("file+1");
            //File.WriteAllText(tempCacheLocation[1] + "cache" + test.Count, mediaInfo.ToString());
        }

        public void SaveCache()
        {
            //JObject cache = JObject.Parse(JsonConvert.SerializeObject(mediaInfoCache));
            DLog.Log("saving count : " + test.Count);
            //DLog.Log("saving count : " + mediaInfoCache.Count);

            //File.WriteAllText(tempCacheLocation[dump] + "cache", test.Count + "");
        }
    }
}
