﻿using System.IO;

namespace Phase4
{
    public sealed class Thumnail
    {
        public string FileFullpath { get; set; }
        public string FileName { get; set; }

        public Thumnail(string file_fullpath)
        {
            this.FileFullpath = file_fullpath;
            this.FileName = Path.GetFileName(file_fullpath);
        }
    }
}
