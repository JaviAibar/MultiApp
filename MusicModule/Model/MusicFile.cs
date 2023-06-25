using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicModule.Model
{
    public class MusicFile : BindableBase
    {

        private string _path;
        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MusicFile(string path, string title)
        {
            Path = path;
            Title  = title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
