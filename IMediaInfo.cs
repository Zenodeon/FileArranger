using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileArranger
{
    interface IMediaInfo
    {
        public FileInfo fileInfo { get; set; }


        public string title { get { return fileInfo.Name.Split('.')[0]; } }

        public string extention { get { return fileInfo.Extension.Split('.')[1]; } }

    }
}
