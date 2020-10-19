using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Services.Email.BaseModels;

namespace WorkMyTerritory.Services.Email.BaseInterfaces
{
    public interface IEmailService
    {
        //Normal Methods
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);

        //Async Methods
        Task SendAsync(EmailMessage emailMessage);

        Task<List<EmailMessage>> RecieveEmailAsync(int maxCount = 10);
    }
}
