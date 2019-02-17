using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismInfrastructure;
using Status;
using Status.Views;
using Unity;

namespace PrismDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";

        private readonly IRegionManager _regionManager;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);

            _regionManager.RegisterViewWithRegion(RegionNames.MainStatusRegion, () => container.Resolve<ViewA>());
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.MainContentRegion, navigatePath);
        }
    }
}
