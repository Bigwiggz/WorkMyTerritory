using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Services.Email.BaseModels;

namespace WorkMyTerritory.Services.Email.BaseInterfaces
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
