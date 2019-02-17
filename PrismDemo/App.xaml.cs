using PrismDemo.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using ModuleA;
using ModuleB;
using Status;
using PrismInfrastructure.Interfaces;
using PrismInfrastructure.Services.Imap;
using Email;

namespace PrismDemo
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
            containerRegistry.RegisterInstance<IMail>(new MailClient("", 999, "", ""));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<ModuleAModule>();
            moduleCatalog.AddModule<ModuleBModule>();
            moduleCatalog.AddModule<StatusModule>();
            moduleCatalog.AddModule<EmailModule>();
        }
    }
}
