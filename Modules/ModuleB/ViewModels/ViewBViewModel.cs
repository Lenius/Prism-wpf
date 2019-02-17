using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using PrismInfrastructure.Events;

namespace ModuleB.ViewModels
{
    public class ViewBViewModel : BindableBase, IConfirmNavigationRequest
    {
        private string _message;

        IEventAggregator _ea;


        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewBViewModel(IEventAggregator ea)
        {
            _ea = ea;

            Message = "View B from your Prism Module";
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

           // if (MessageBox.Show("Do you to navigate?", "Navigate?", MessageBoxButton.YesNo) == MessageBoxResult.No)
           //     result = false;

            continuationCallback(result);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _ea.GetEvent<StatusEvent>().Publish("Showing Module B");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }
    }
}
