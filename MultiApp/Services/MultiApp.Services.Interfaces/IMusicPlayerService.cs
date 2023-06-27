using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MultiApp.Services.Interfaces
{
    public interface IMusicPlayerService
    {
        public string CurrentSong { get; set; }
        public void Play();
        public void Pause();
        public void Stop();

        public bool IsPlaying { get; }

        public void AddMediaEndedListener(EventHandler mediaEnded);

        public void RemoveMediaEndedListener(EventHandler mediaEnded);
    }
}
