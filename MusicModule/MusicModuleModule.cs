using MultiApp.Core;
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

            IRegion region = regionManager.Regions[RegionNames.MusicRegion];
            var player = containerProvider.Resolve<MusicPlayer>();
            region.Add(player);

            region = regionManager.Regions[RegionNames.PlayerRegion];
            var playerView = containerProvider.Resolve<PlayerView>();
            region.Add(playerView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {


        }
    }
}