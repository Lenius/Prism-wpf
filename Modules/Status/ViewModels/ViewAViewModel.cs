using Prism.Events;
using Prism.Mvvm;
using System;
using PrismInfrastructure.Events;

namespace Status.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel(IEventAggregator eventAggregator)
        {
            Message = "No status";

            //eventAggregator.GetEvent<StatusEvent>().Subscribe(StatusEventUpdated, ThreadOption.PublisherThread, false, (filter) => filter.Contains("Brian"));
            eventAggregator.GetEvent<StatusEvent>().Subscribe(StatusEventUpdated);
        }

        private void StatusEventUpdated(string obj)
        {
            Message = obj + " : " + DateTime.Now.ToString();
        }
    }
}
