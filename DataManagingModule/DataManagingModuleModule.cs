using DataManagingModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DataManagingModule
{
    public class DataManagingModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
            var dataManagingRegion = regionManager.Regions["DataManagingRegion"];
            var view = containerProvider.Resolve<DataList>();
            dataManagingRegion.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}