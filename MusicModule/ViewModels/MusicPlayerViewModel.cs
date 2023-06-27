using MultiApp.Services;
using MultiApp.Services.Interfaces;
using MusicModule.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicModule.ViewModels
{
    public class MusicPlayerViewModel : BindableBase
    {
        // Services
        private IMusicPlayerService _musicService;

        // Delegate backing fields
        private DelegateCommand _loadMusic;

        // Delegate Commands
        public DelegateCommand LoadMusicFiles =>
            _loadMusic ?? (_loadMusic = new DelegateCommand(ExecuteLoadMusicFiles));
        public DelegateCommand<MusicFile> SongSelectedCommand { get; private set; }
        public MusicFile SongFileSelected
        {
            get { return _songSelected; }
            set { SetProperty(ref _songSelected, value); }
        }

        // Properties backing field
        private string _title;

        private MusicFile _songSelected;
        private ObservableCollection<MusicFile> _musicList = new();
        
        // Observable Properties
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<MusicFile> MusicList
        {
            get { return _musicList; }
            set { SetProperty(ref _musicList, value); }
        }



        public MusicPlayerViewModel(IMusicPlayerService musicPlayerService)
        {
            _musicService = musicPlayerService;
            SongSelectedCommand = new DelegateCommand<MusicFile>(SongSelected);
            ExecuteLoadMusicFiles();
        }
        private void SongSelected(MusicFile song)
        {
            if (song == null) return;
            _songSelected = song;
            _musicService.CurrentSong = song.Path;
        }

        void ExecuteLoadMusicFiles()
        {
            MusicList.Clear();

            FileInfo[] files = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)).GetFiles()
                .Where(f => IsAudioFile(f.Name))
                .ToArray();

            foreach (FileInfo file in files)
            {
                MusicList.Add(new MusicFile(file.FullName, file.Name));
            }
        }
        public bool IsAudioFile(string name)
        {
            return EndsWith(name, new string[] { ".mp3", ".wav" }); //, ".ogg" not supported by WPF
        }

        public static bool EndsWith(string name, string[] exts)
        {
            foreach (string ext in exts)
                if (name.EndsWith(ext, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        
        
    }
}
