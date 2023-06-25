using System;
using System.Collections.Generic;
using System.Text;

namespace MultiApp.Services.Interfaces
{
    public interface IMusicPlayerService
    {
        public string CurrentSong { get; set; }
        public void Play();
    }
}
