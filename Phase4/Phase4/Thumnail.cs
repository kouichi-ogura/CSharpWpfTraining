using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Phase4
{
    public sealed class Thumnail
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public Thumnail(string filepath)
        {
            this.FilePath = filepath;
            this.FileName = Path.GetFileName(filepath);
        }
    }
}
