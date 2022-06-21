using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace RegistraPontoService
{
    public partial class SendMail
    {
        public static void EnviaEmailResponsavel()
        {
            StringBuilder body = new StringBuilder();

            try
            {                
                body.AppendLine("Prezado,");
                body.AppendLine("Ponto Registrado com sucesso em: " + DateTime.Now.ToString());
                body.AppendLine("<hr>");


                //Send("smtp.devtime.net.br", 587, "admin=devtime.net.br", "devtime2022", body, "Plano de Ação", "admin@devtime.net.br", "Email Automático (Plano de Ação)", true, true, response.Request.eMailResponsavel);

                Envia_com_anexo("smtp.office365.com", 587, "lerron.jesus@transire.com", "Tr@nsire2031", body, "Registra Ponto - Conecthus", "lerron.jesus@transire.com", "Email Automático (Bot)", true, true, "lerron.jesus@conecthus.org.br");

            }
            catch (Exception ex)
            {
                body = new StringBuilder();
                body.AppendLine("Prezado,");
                body.AppendLine("Ponto Registrado com sucesso em: " + DateTime.Now.ToString());
                body.AppendLine("<hr>");

                Envia_sem_anexo("smtp.office365.com", 587, "lerron.jesus@transire.com", "Tr@nsire2031", body, "Registra Ponto - Conecthus", "lerron.jesus@transire.com", "Email Automático (Bot)", true, true, "lerron.jesus@conecthus.org.br");
            }
        }

        public static void Envia_com_anexo(string host, int port, string smtpUser, string smptPass, StringBuilder body, string subject, string senderMail, string senderName, bool requireConfirmation, bool requireNotifications, string destinatario)
        {
            MailAddress fromAddress = new MailAddress(senderMail, senderName);

            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(smtpUser, smptPass)
                //,Timeout = 20
            };

            var toAddress = new MailAddress(destinatario);

            body.AppendLine("*** Com Anexo ***");
            body.AppendLine("<hr>");
            string sBody = body.ToString();
            sBody = sBody.Replace("\r\n", "\r");
            sBody = sBody.Replace("\n", "\r");
            sBody = sBody.Replace("\r", "<br>\r\n");
            sBody = sBody.Replace("  ", "  ");
            
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = sBody,
                IsBodyHtml = true,
            })
            {
                if (requireNotifications)
                {
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure;
                }

                if (requireConfirmation)
                {
                    message.Headers.Add("Read-Receipt-To", senderMail);
                    message.Headers.Add("Return-Receipt-To", senderMail);
                    message.Headers.Add("Disposition-Notification-To", senderMail);
                }

                DateTime dt = DateTime.Now;

                var contentID = "Image";
                var inlineLogo = new Attachment(@"C:\PCFCustom\Projetos\ScreenRegistro-" + dt.DayOfWeek + ".png");
                inlineLogo.ContentId = contentID;
                inlineLogo.ContentDisposition.Inline = true;
                inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                message.Attachments.Add(inlineLogo);
                message.Body += "<br /><br /><img src=\"cid:" + contentID + "\" height=\"500\" width=\"500\"><br />";

                smtp.Send(message);
            }
        }

        public static void Envia_sem_anexo(string host, int port, string smtpUser, string smptPass, StringBuilder body, string subject, string senderMail, string senderName, bool requireConfirmation, bool requireNotifications, string destinatario)
        {
            MailAddress fromAddress = new MailAddress(senderMail, senderName);

            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(smtpUser, smptPass)
                //,Timeout = 20
            };

            var toAddress = new MailAddress(destinatario);

            body.AppendLine("*** Sem anexo, verifique o log de execução ! ***");
            body.AppendLine("<hr>");
            string sBody = body.ToString();
            sBody = sBody.Replace("\r\n", "\r");
            sBody = sBody.Replace("\n", "\r");
            sBody = sBody.Replace("\r", "<br>\r\n");
            sBody = sBody.Replace("  ", "  ");

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = sBody,
                IsBodyHtml = true,
            })
            {
                if (requireNotifications)
                {
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure;
                }

                if (requireConfirmation)
                {
                    message.Headers.Add("Read-Receipt-To", senderMail);
                    message.Headers.Add("Return-Receipt-To", senderMail);
                    message.Headers.Add("Disposition-Notification-To", senderMail);
                }

                smtp.Send(message);
            }
        }
    }
}

