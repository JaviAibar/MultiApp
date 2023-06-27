using MultiApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MultiApp.Services
{
    public class MusicPlayerService : IMusicPlayerService
    {
        public string? CurrentSong { get; set; }

        private MediaPlayer mediaPlayer = new MediaPlayer();

        public void Play()
        {
            if (CurrentSong == null) return;
            mediaPlayer.Open(new Uri(CurrentSong));
            mediaPlayer.Play();
        }
    }
}
