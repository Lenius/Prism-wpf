using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismInfrastructure.Events;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase, IConfirmNavigationRequest
    {
        private string _message;

        IEventAggregator _ea;

        public DelegateCommand SendMessageCommand { get; private set; }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel(IEventAggregator ea)
        {
            _ea = ea;

            Message = "View A from your Prism Module";

            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _ea.GetEvent<StatusEvent>().Publish(Message);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _ea.GetEvent<StatusEvent>().Publish("Showing Module A");
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
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
