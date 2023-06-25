using MultiApp.Modules.ModuleName;
using MultiApp.Services;
using MultiApp.Services.Interfaces;
using MultiApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace MultiApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<IMusicPlayerService, MusicPlayerService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
           // moduleCatalog.AddModule<ModuleNameModule>();
            moduleCatalog.AddModule<MusicModule.MusicModuleModule>();
            moduleCatalog.AddModule<Visualizer3D.Visualizer3DModule>();
            moduleCatalog.AddModule<DataManagingModule.DataManagingModuleModule>();
        }
    }
}
