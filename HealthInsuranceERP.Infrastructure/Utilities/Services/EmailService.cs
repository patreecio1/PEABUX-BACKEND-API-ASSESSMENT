using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Enums;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppUrl _appUrl;
        private readonly MailConfiguration _mailSettings;
        private readonly ILoggerService<EmailService> _loggerService;
        private readonly IEmailLogRepository emailLogRepository;
        public EmailService(IEmailLogRepository emailLogRepositor, AppUrl appUrl, MailConfiguration mailConfiguration, ILoggerService<EmailService> loggerService)
        {
            this._appUrl = appUrl;
            _mailSettings = mailConfiguration;
            _loggerService = loggerService;
            emailLogRepository = emailLogRepositor;
        }


        public async Task<bool> Send(EmailRequestModel model)
        {
            // string master = await File.ReadAllTextAsync(Constants.MasterTemplate);
            //master = master.Replace("[Content]", model.Body);
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            try
            {
                message.IsBodyHtml = true;
                MailAddress fromAddress = new MailAddress(_mailSettings.MailFrom, model.SenderDisplayName ?? _mailSettings.MailFromName);
                message.From = fromAddress;


            
                    foreach (var to in model.DestinationEmail)
                    {
                        message.To.Add(new MailAddress(to.Key, to.Value));
                    }
           
                    foreach (var to in model.Bcc)
                    {
                        message.Bcc.Add(new MailAddress(to.Key, to.Value));
                    }

                
               
                    foreach (var to in model.Cc)
                    {
                        message.CC.Add(new MailAddress(to.Key, to.Value));
                    }
                _loggerService.LogInformation("About to compose attachment");

                for (int i = 0; i < model.Attachement.Length; i++)
                {
                    try
                    {
                        if (i == 1)
                        {
                            string atta = model.Attachement[i];
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(model.Attachement[1]);
                            //  var file = new FileModel($"Attachment{i + 1}", attch, new MemoryStream(bytes));
                            var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);

                            Attachment att = new Attachment(new MemoryStream(buffer), contentType);
                            att.ContentDisposition.FileName = $"Attachment{i + 1}" + ".pdf";
                            message.Attachments.Add(att);
                        }
                        else
                        {
                            string attch = model.Attachement[i];

                            byte[] bytes = ConvertStringToByteArray(attch);// System.IO.File.ReadAllBytes(attch);


                            //  var file = new FileModel($"Attachment{i + 1}", attch, new MemoryStream(bytes));
                            var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);

                            Attachment att = new Attachment(new MemoryStream(bytes), contentType);
                            att.ContentDisposition.FileName = $"Attachment{i + 1}" + ".pdf";
                            message.Attachments.Add(att);

                        }
                        //message.Attachments.Add(new Attachment(file.Content, $"Attachment{i + 1}{file.Extension}"));
                    }
                    catch (Exception ex)
                    {
                        _loggerService.LogInformation("Error during attaching");
                        _loggerService.LogError(ex);
                    }
                }

                message.Subject = model.Subject;
                message.Body = model.Body;// master;
                message.IsBodyHtml = true;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = _mailSettings.SMTPServer;
                smtpClient.Port = _mailSettings.SMTPPort;
               
                smtpClient.Send(message);

                try
                {
                    await emailLogRepository.Create(new EmailLogDto
                    {
                        Recipient = message.To[0].Address,
                        Subject = message.Subject,
                        MailBody = message.Body,
                        MailFromName = message.From.DisplayName,
                        MailFrom = message.From.Address,
                        Date = DateTime.Now,
                        Status = "Succesful",
                        SMTPServer = _mailSettings.SMTPServer,
                        SMTPPort = _mailSettings.SMTPServer,
                        StatusMessage = "Succesful"


                    });
                }
                catch (Exception ex) { _loggerService.LogError(ex); }
                return true;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);

                try
                {
                    await emailLogRepository.Create(new EmailLogDto
                    {
                        Recipient = message.To[0].Address,
                        Subject = message.Subject,
                        MailBody = message.Body,
                        MailFromName = message.From.DisplayName,
                        MailFrom = message.From.Address,
                        Date = DateTime.Now,
                        Status = "Failed",
                        SMTPServer = _mailSettings.SMTPServer,
                        SMTPPort = _mailSettings.SMTPServer,
                        StatusMessage = ex.Message



                    });
                }
                catch (Exception exx) { _loggerService.LogError(exx); }

                return false;
            }
            finally
            {
                smtpClient.Dispose();
                message.Dispose();
            }
        }
        private byte[] ConvertStringToByteArray(string byteString)
        {
            // string byteString = "48-65-6C-6C-6F";
            string[] strArray = byteString.Split('-');
            byte[] byteArray = new byte[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                byteArray[i] = Convert.ToByte(strArray[i], 16);
            }
            return byteArray;

        }
    }
}
