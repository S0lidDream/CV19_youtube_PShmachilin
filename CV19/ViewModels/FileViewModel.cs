using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CV19.ViewModels
{
    class FileViewModel : ViewModel
    {
        private readonly FileInfo _FileInfo;
        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;

        public DateTime CreationTime => _FileInfo.CreationTime;

        public FileViewModel(string path)
        {
            _FileInfo = new FileInfo(path);
        }
    }
}
