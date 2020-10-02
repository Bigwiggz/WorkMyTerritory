using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Models;

namespace WorkMyTerritory.Services
{
    public interface IEmailTerritoryService
    {
        public void Send(EmailMessage emailMessage);
        public List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
