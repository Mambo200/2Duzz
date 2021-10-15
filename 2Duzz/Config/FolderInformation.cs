using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Duzz.Config
{
    public struct FolderInformation
    {
        public string folder;
        public bool subfolders;

        public FolderInformation(string _folder, bool _includeSubfolders)
        {
            folder = _folder;
            subfolders = _includeSubfolders;
        }
    }
}
