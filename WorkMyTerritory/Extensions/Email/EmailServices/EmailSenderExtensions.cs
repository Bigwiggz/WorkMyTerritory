using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WorkMyTerritory.Extensions.Email.EmailInterfaces;
using WorkMyTerritory.Models;
using WorkMyTerritory.Services.Email.BaseInterfaces;
using WorkMyTerritory.Services.Email.BaseModels;

namespace WorkMyTerritory.Extensions.Email.EmailServices
{
    public class EmailSenderExtensions : IEmailSenderExtensions
    {
        private readonly IEmailService _emailService;

        public EmailSenderExtensions(IEmailService emailService)
        {
           _emailService = emailService;
        }
        public void SendEmailConfirmation(ApplicationUser user, string link)
        {
            //Bring in email Model from Base Email Model
            EmailAddress sendToEmailAddress = new EmailAddress()
            {
                Email = user.Email,
                FirstName=user.PublisherFirstName,
                LastName=user.PublisherLastName
            };

            //Create a list of email address models
            List<EmailAddress> listofSendtoEmailAddresses = new List<EmailAddress>();

            listofSendtoEmailAddresses.Add(sendToEmailAddress);

            //Add in Message information
            string linkInfo = $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>";

            //Add new EmailMessage from Base EMail Message
            EmailMessage sendtoEmailMessage = new EmailMessage()
            {
                Subject = linkInfo,
                Content = "",
                Header = "",
                ToAddresses = listofSendtoEmailAddresses
            };

            //Send email
            _emailService.Send(sendtoEmailMessage);
        }
    }
}
