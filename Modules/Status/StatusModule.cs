using Status.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Status
{
    public class StatusModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>("Status");
        }
    }
}