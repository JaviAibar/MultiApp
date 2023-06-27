using MultiApp.Services.Interfaces;
using MusicModule.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace MusicModule.ViewModels
{
    public class PlayerViewModel : BindableBase
    {
        // Services
        private IMusicPlayerService _musicService;

        // Delegate backing fields
        private DelegateCommand _playCurrentSound;
        private DelegateCommand _playNextSound;
        private DelegateCommand _playPreviousSound;

        // Delegate Commands
        public DelegateCommand PlayCurrentSound =>
            _playCurrentSound ?? (_playCurrentSound = new DelegateCommand(ExecutePlayCurrentSound));
        public DelegateCommand PlayNextSound =>
            _playNextSound ?? (_playNextSound = new DelegateCommand(ExecuteNotImplemented));
        public DelegateCommand PlayPreviousSound =>
            _playPreviousSound ?? (_playPreviousSound = new DelegateCommand(ExecuteNotImplemented));



        public PlayerViewModel(IMusicPlayerService musicService)
        {
            _musicService = musicService;
        }


        void ExecutePlayCurrentSound()
        {
            _musicService.Play();
        }


        // TODO: Implement previous/next songs feature in PlayerViewModel
        void ExecuteNotImplemented()
        {
            MessageBox.Show("Sorry this feature is not implemented yet.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void ExecutePlayNextSound()
        {

        }

        void ExecutePlayPreviousSound()
        {

        }
    }
}
