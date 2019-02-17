using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Prism.Events;
using PrismInfrastructure.Events;

namespace Email.ViewModels
{
    public class EmailListViewModel : BindableBase, INavigationAware
    {

        private readonly IMail _mail;
        private readonly IEventAggregator _ea;
        private List<PrismInfrastructure.Models.Email> _orders;

        public DelegateCommand<PrismInfrastructure.Models.Email> DownloadCommand { get; set; }


        private bool _isBusy = true;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        
        public EmailListViewModel(IMail mail, IEventAggregator ea)
        {
            _mail = mail;
            _ea = ea;
            
            DownloadCommand = new DelegateCommand<PrismInfrastructure.Models.Email>(DownloadAttatchementCommand, CanDownloadAttachement);
        }

        private void DownloadAttatchementCommand(PrismInfrastructure.Models.Email obj)
        {
            var i = obj;

            var dialog = new SaveFileDialog()
            {
                Filter = "Csv Files(*.csv)|*.csv",
                FileName = obj.Message.Attachments[0].Name
            };

            if (dialog.ShowDialog() == true)
            {
                SaveMailAttachment(obj.Message.Attachments[0], dialog.FileName);
            }
        }

        private void SaveMailAttachment(Attachment attachment, string destinationFile)
        {
            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }

            byte[] allBytes = new byte[attachment.ContentStream.Length];
            int bytesRead = attachment.ContentStream.Read(allBytes, 0, (int)attachment.ContentStream.Length);

            BinaryWriter writer = new BinaryWriter(new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None));
            writer.Write(allBytes);
            writer.Close();
        }

        private bool CanDownloadAttachement(PrismInfrastructure.Models.Email arg)
        {
            return arg.Message.Attachments.Count == 1;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetMails();
        }

        public List<PrismInfrastructure.Models.Email> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value); }
        }

        private void LoadDb()
        {
            Orders = _mail.GetMails();

            _ea.GetEvent<StatusEvent>().Publish("Emails : " + Orders.Count.ToString());

            IsBusy = false;
        }

        private void GetMails()
        {

            _ea.GetEvent<StatusEvent>().Publish("Henter Emails");

            IsBusy = true;

            Task.Run(() =>
            {
                LoadDb();
            });

        }
    }
}
