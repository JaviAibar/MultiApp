using DataManagingModule.Views;
using MultiApp.Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace DataManagingModule
{
    public class DataManagingModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
            var dataManagingRegion = regionManager.Regions[RegionNames.DataManagingRegion];
            var view = containerProvider.Resolve<DataList>();
            dataManagingRegion.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DataEditView>();
            containerRegistry.RegisterForNavigation<DataList>();
        }
    }
}