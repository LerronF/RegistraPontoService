using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RegistraPontoService
{
    public partial class SendMail
    {
        public static void EnviaEmailResponsavel()
        {
            try
            {
                StringBuilder body = new StringBuilder();
                body.AppendLine("Prezado,");
                body.AppendLine("Ponto Registrado com sucesso: " + DateTime.Now.ToString());
                body.AppendLine("<hr>");


                //Send("smtp.devtime.net.br", 587, "admin=devtime.net.br", "devtime2022", body, "Plano de Ação", "admin@devtime.net.br", "Email Automático (Plano de Ação)", true, true, response.Request.eMailResponsavel);

                Send("smtp.office365.com", 587, "lerron.jesus@transire.com", "Tr@nsire2030", body, "Registra Ponto - Conecthus", "lerron.jesus@transire.com", "Email Automático (Bot)", true, true, "lerron.jesus@conecthus.org.br");

            }
            catch (Exception ex)
            {
                
            }
        }

        public static void Send(string host, int port, string smtpUser, string smptPass, StringBuilder body, string subject, string senderMail, string senderName, bool requireConfirmation, bool requireNotifications, string destinatario)
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

            string sBody = body.ToString();
            sBody = sBody.Replace("\r\n", "\r");
            sBody = sBody.Replace("\n", "\r");
            sBody = sBody.Replace("\r", "<br>\r\n");
            sBody = sBody.Replace("  ", "  ");

            //if (requireTracker)
            //{
            //    sBody = sBody.Insert(sBody.IndexOf("<hr>") + 4, string.Format("<br><br><a href=\"{0}/ServiceMailerWeb/URLTracker.aspx?AlrCode='{1}'\">Clique aqui para confirmar o recebimento do e-mail</a>", webServer, alrCode));

            //    sBody = sBody.Insert(sBody.IndexOf("</table>") + 8, string.Format("<br><br><br><br>Atenciosamente,<br><br>{0}<br>{1}", senderName, senderMail));

            //    sBody += string.Format("<br><br><img alt=\"Logo\" src=\"{0}/ServiceMailerWeb/images/{1}.aspx\"><br>", webServer, alrCode);
            //}


            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = sBody,
                IsBodyHtml = true
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

