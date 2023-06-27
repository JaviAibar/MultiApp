using MultiApp.Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Visualizer3D.Views;

namespace Visualizer3D
{
    public class Visualizer3DModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
            IRegion visualizer3DRegion = regionManager.Regions[RegionNames.Visualizer3DRegion];
            var view = containerProvider.Resolve<Visualizer3DView>();
            visualizer3DRegion.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}