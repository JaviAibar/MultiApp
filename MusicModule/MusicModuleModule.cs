using MusicModule.ViewModels;
using MusicModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel;

namespace MusicModule
{
    public class MusicModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            IRegion region = regionManager.Regions["MusicRegion"];
            var player = containerProvider.Resolve<MusicPlayer>();
            (player.DataContext as MusicPlayerViewModel).Title = "Music";
            region.Add(player);

            region = regionManager.Regions["PlayerRegion"];
            var playerView = containerProvider.Resolve<PlayerView>();
            region.Add(playerView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {


        }
    }
}