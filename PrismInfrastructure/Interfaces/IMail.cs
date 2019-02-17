using PrismInfrastructure.Models;
using System.Collections.Generic;

namespace PrismInfrastructure.Interfaces
{
    public interface IMail
    {
        List<Email> GetMails();
        Email GetMessage(uint i);
        void DeleteMessage(Email i);
    }
}
