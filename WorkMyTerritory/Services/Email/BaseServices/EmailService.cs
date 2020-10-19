using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkMyTerritory.Services.Email.BaseInterfaces;
using WorkMyTerritory.Services.Email.BaseModels;

namespace WorkMyTerritory.Services.Email.BaseServices
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
           _emailConfiguration = emailConfiguration;
        }
        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
			using (var emailClient = new Pop3Client())
			{
				emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

				emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

				emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

				List<EmailMessage> emails = new List<EmailMessage>();
				for (int i = 0; i < emailClient.Count && i < maxCount; i++)
				{
					var message = emailClient.GetMessage(i);
					var emailMessage = new EmailMessage
					{
						Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
						Subject = message.Subject
					};
					emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, FullName = x.Name }));
					emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, FullName = x.Name }));
					emails.Add(emailMessage);
				}

				return emails;
			}
		}

        public async Task<List<EmailMessage>> RecieveEmailAsync(int maxCount = 10)
        {
			using (var emailClient = new Pop3Client())
			{
				await emailClient.ConnectAsync(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

				emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

				await emailClient.AuthenticateAsync(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

				List<EmailMessage> emails = new List<EmailMessage>();
				for (int i = 0; i < emailClient.Count && i < maxCount; i++)
				{
					var message = emailClient.GetMessage(i);
					var emailMessage = new EmailMessage
					{
						Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
						Subject = message.Subject
					};
					emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, FullName = x.Name }));
					emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, FullName = x.Name }));
					emails.Add(emailMessage);
				}

				return emails;
			}
		}

        public void Send(EmailMessage emailMessage)
        {
			var message = new MimeMessage();
			message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.FullName, x.Email)));
			message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.FullName, x.Email)));

			message.Subject = emailMessage.Subject;
			
			//We will say we are sending HTML. But there are options for plaintext etc. 
			//These lines are for attachments
			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = string.Format(emailMessage.Content)
			};

			if(message.Attachments!=null && message.Attachments.Any())
            {
				byte[] fileBytes;
				foreach(var attachment in message.Attachments)
                {
					using(var ms = new MemoryStream())
                    {
						attachment.WriteTo(ms);
						fileBytes = ms.ToArray();
                    }
					bodyBuilder.Attachments.Add(attachment.ContentDisposition.FileName, fileBytes, attachment.ContentType);
                }
            }

			message.Body = bodyBuilder.ToMessageBody();

			//When using an attachment, you need to create the files object you want to send by adding the following line in the controller
			//var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

			//Given way to send email without an attachment
			/*
			message.Body = new TextPart(TextFormat.Html)
			{
				Text = emailMessage.Content
			};
            */


			//Be careful that the SmtpClient class is the one from Mailkit not the framework!
			using (var emailClient = new SmtpClient())
			{
				try
				{
					//The last parameter here is to use SSL (Which you should!)
					emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

					//Remove any OAuth functionality as we won't be using it. 
					emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

					emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

					emailClient.Send(message);
				}
				catch
				{
					throw;
				}
				finally
				{
					emailClient.Disconnect(true);
					emailClient.Dispose();
				}
				
			}
		}

		public async Task SendAsync(EmailMessage emailMessage)
		{
			var message = new MimeMessage();
			message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.FullName, x.Email)));
			message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.FullName, x.Email)));

			message.Subject = emailMessage.Subject;

			//We will say we are sending HTML. But there are options for plaintext etc. 
			//These lines are for attachments
			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = string.Format(emailMessage.Content)
			};

			if (message.Attachments != null && message.Attachments.Any())
			{
				byte[] fileBytes;
				foreach (var attachment in message.Attachments)
				{
					using (var ms = new MemoryStream())
					{
						attachment.WriteTo(ms);
						fileBytes = ms.ToArray();
					}
					bodyBuilder.Attachments.Add(attachment.ContentDisposition.FileName, fileBytes, attachment.ContentType);
				}
			}

			message.Body = bodyBuilder.ToMessageBody();

			//When using an attachment, you need to create the files object you want to send by adding the following line in the controller
			//var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

			//Given way to send email without an attachment
			/*
			message.Body = new TextPart(TextFormat.Html)
			{
				Text = emailMessage.Content
			};
            */


			//Be careful that the SmtpClient class is the one from Mailkit not the framework!
			using (var emailClient = new SmtpClient())
			{
				try
				{
					//The last parameter here is to use SSL (Which you should!)
					await emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

					//Remove any OAuth functionality as we won't be using it. 
					emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

					await emailClient.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

					await emailClient.SendAsync(message);
				}
				catch
				{
					throw;
				}
				finally
				{
					await emailClient.DisconnectAsync(true);
					emailClient.Dispose();
				}
			}
		}
    }
}
