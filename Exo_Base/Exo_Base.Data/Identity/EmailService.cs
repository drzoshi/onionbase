using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Exo_Base.Data.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration("~/web.config");
            MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            if (mailSettings == null)
                throw new Exception("SMTP-Server is not configured properly.");

            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = mailSettings.Smtp.Network.Host;
                client.Port = mailSettings.Smtp.Network.Port;
                client.EnableSsl = mailSettings.Smtp.Network.EnableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailSettings.Smtp.Network.UserName, mailSettings.Smtp.Network.Password);

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(mailSettings.Smtp.Network.UserName),
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };
                mail.To.Add(new MailAddress(message.Destination));

                client.Send(mail);
                return Task.FromResult(0);
            }
            catch (SmtpException ex)
            {
                Trace.TraceInformation(ex.Message);
                return Task.FromResult(1);
                //throw new Exception("The Server is not able to complete a email sending operation");
            }
        }
    }
}
