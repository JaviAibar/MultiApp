using MultiApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MultiApp.Services
{
    public class MusicPlayerService : IMusicPlayerService
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public string CurrentSong { get; set; }

        public bool IsPlaying { get; private set; }

        public void Play()
        {
            if (CurrentSong == null) return;
            mediaPlayer.Open(new Uri(CurrentSong));
            mediaPlayer.Play();
            IsPlaying = true;
        }

        public void Stop() { mediaPlayer.Stop(); IsPlaying = false; }
        public void Pause() { mediaPlayer.Pause(); IsPlaying = false; }

        public void AddMediaEndedListener(EventHandler mediaEnded)
        {
            mediaPlayer.MediaEnded += mediaEnded;
        }

        public void RemoveMediaEndedListener(EventHandler mediaEnded)
        {
            mediaPlayer.MediaEnded += mediaEnded;
        }
    }
}
