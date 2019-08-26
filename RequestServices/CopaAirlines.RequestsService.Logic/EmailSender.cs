using CopaAirlines.RequestsService.Common;
using CopaAirlines.RequestsService.Interface;
using CopaAirlines.RequestsService.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace CopaAirlines.RequestsService.Logic
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger logger;

        public EmailSender(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger("CopaAirlines.RequestService.Logic.EmailSender");
        }

        public bool Send(EmailViewModel emailModel, string pathFile = null)
        {
            MailMessage oMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(emailModel.SMTPServer);
            bool result = false;
            try
            {
                smtpClient.EnableSsl = false;
                oMessage.From = new MailAddress(emailModel.From);

                if(!string.IsNullOrEmpty(pathFile))
                {
                    if (pathFile.Trim() != "")
                    {
                        if (System.IO.File.Exists(pathFile))
                            oMessage.Attachments.Add(new Attachment(pathFile));
                    }
                }
                
                var xemail = emailModel.To.Split(new[] { ';' });
                if (xemail.Length > 1)
                {
                    foreach (var item in xemail)
                        oMessage.To.Add(new MailAddress(item));
                }
                else
                    oMessage.To.Add(new MailAddress(emailModel.To));

                oMessage.Subject = emailModel.Subject;
                oMessage.IsBodyHtml = false;
                oMessage.Body = emailModel.Body;
                
                smtpClient.Send(oMessage);
                result = true;
            }
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
            }
            finally
            {
                File.Delete(@pathFile);
            }

            return result;
        }
    }
}
