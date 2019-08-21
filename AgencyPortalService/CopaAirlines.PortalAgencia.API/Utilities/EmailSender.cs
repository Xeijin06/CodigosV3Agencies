using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Portaldeagencias.Utilities
{
    public static class EmailSender
    {
        public static bool SendMessageNotification(string pathFile, string caseID, string agencyName, string iATACode, string agencyPhoneNumber, string agencyEmail, string country, string language, string description, string ContactPhoneNumber,string PNR)
        {
            try
            {
                string messageBody = File.ReadAllText(Path.Combine(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RutaEmailTemplate"]), "emailTemplate.txt"));

                string From = ConfigurationManager.AppSettings.Get("Email_From");
                string To = ConfigurationManager.AppSettings.Get("Email_To");
                string Host = ConfigurationManager.AppSettings.Get("SMTPServer");
                string EmailSubject = ConfigurationManager.AppSettings.Get("EmailSubject");

                MailMessage oMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(Host);
                smtpClient.EnableSsl = false;
                oMessage.From = new MailAddress(From);

                /*caseID = HttpUtility.HtmlEncode(caseID);
                agencyName = HttpUtility.HtmlEncode(agencyName);
                iATACode = HttpUtility.HtmlEncode(iATACode);
                agencyPhoneNumber = HttpUtility.HtmlEncode(agencyPhoneNumber);
                agencyEmail = HttpUtility.HtmlEncode(agencyEmail);
                country = HttpUtility.HtmlEncode(country);
                language = HttpUtility.HtmlEncode(language);
                description = HttpUtility.HtmlEncode(description);*/

                Dictionary<string, string> replacements = new Dictionary<string, string>(){
                    {"{caseID}", caseID}, {"{agencyName}", agencyName }, {"{iATACode}", iATACode},{"{agencyEmail}", agencyEmail},{"{ContactPhoneNumber}", ContactPhoneNumber},
                    { "{agencyPhoneNumber}", agencyPhoneNumber}, {"{country}", country}, {"{language}", language}, {"{description}", description},{ "{PNR}",PNR}
                                                };

                messageBody = replacements.Aggregate(messageBody, (result, s) => result.Replace(s.Key, s.Value));


                        if (pathFile.Trim() != "")
                        {
                            if (System.IO.File.Exists(pathFile))
                                oMessage.Attachments.Add(new Attachment(pathFile));
                        }

                var xemail = To.Split(new[] { ';' });

                if (xemail.Length > 1)
                {
                    foreach (var item in xemail)
                        oMessage.To.Add(new MailAddress(item));
                }
                else
                    oMessage.To.Add(new MailAddress(To));

                oMessage.Subject = string.Format(EmailSubject,caseID, language);
                oMessage.IsBodyHtml = false;
                oMessage.Body = messageBody;


                smtpClient.Send(oMessage);

                
                if (oMessage.Attachments != null)
                {
                    for (int i = oMessage.Attachments.Count - 1; i >= 0; i--)
                    {
                        oMessage.Attachments[i].Dispose();
                    }
                    oMessage.Attachments.Clear();
                    oMessage.Attachments.Dispose();
                }
                oMessage.Dispose();
                oMessage = null;

                //smtpClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}