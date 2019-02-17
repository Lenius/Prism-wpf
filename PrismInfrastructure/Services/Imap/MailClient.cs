using PrismInfrastructure.Interfaces;
using PrismInfrastructure.Models;
using S22.Imap;
using System.Collections.Generic;
using System.IO;

namespace PrismInfrastructure.Services.Imap
{
    public class MailClient : IMail
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;
        private ImapClient _connectionImapClient;

        public MailClient(string hostname, int port, string user, string password)
        {
            _hostname = hostname;
            _port = port;
            _user = user;
            _password = password;
        }

        private ImapClient Connection => _connectionImapClient ?? (_connectionImapClient = new ImapClient(_hostname, _port, _user, _password, AuthMethod.Login, true));


        public void DeleteMessage(Email i)
        {
            using (var client = Connection)
            {
                client.DeleteMessage(i.Key);
            }
        }

        public List<Email> GetMails()
        {
            List<Email> messages = new List<Email>();

            using (
                ImapClient client = Connection)
            {
                // This returns *ALL* messages in the inbox.
                IEnumerable<uint> uids =
                    client.Search(
                        SearchCondition.From("emaildok@creinhardt.dk").And(SearchCondition.Subject("Varefil fra C. Reinhardt A/S")));

                foreach (uint uid in uids)
                {
                    var mes = client.GetMessage(uid, FetchOptions.Normal, false);
                    messages.Add(new Email() { Key = uid, Message = mes });
                }

            }

            return messages;
        }

        public Email GetMessage(uint uid)
        {
            using (var client = new ImapClient(_hostname, _port, _user, _password, AuthMethod.Login, true))
            {
                return new Email() { Key = uid, Message = client.GetMessage(uid, FetchOptions.Normal, false) };
            }
        }

        public void SaveMailAttachment(System.Net.Mail.Attachment attachment)
        {
            byte[] allBytes = new byte[attachment.ContentStream.Length];
            int bytesRead = attachment.ContentStream.Read(allBytes, 0, (int)attachment.ContentStream.Length);

            //save files in attchments folder
            string destinationFile = @"c:\tmp\" + attachment.Name;

            BinaryWriter writer = new BinaryWriter(new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None));
            writer.Write(allBytes);
            writer.Close();
        }
    }
}
