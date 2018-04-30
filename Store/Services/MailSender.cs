using System.Net;
using System.Net.Mail;

namespace Store.Services
{
    public class MailSender : IMailSender
    {
        private const string _smtpServer = "smtp.gmail.com";
        private const int _port = 587;
        private const string _user = "store.asp.net@gmail.com";
        private const string _password = "store-store";

        public bool Send(string toAddress, string subject, string body)
        {
            var client = new SmtpClient
            {
                Host = _smtpServer,
                Port = _port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_user, _password)
            };
            var message = new MailMessage { From = new MailAddress(_user) };
            message.To.Add(toAddress);
            message.Body = body;
            message.Subject = subject;

            try
            {
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendStandardEmailConfirm(string address, string token)
        {
            var subject = "Подтверждение регистрации";
            var body = $"Для подтверждения EMail перейдите по ссылке http://localhost:5001/Account/ConfirmEmail?token={token}";
            return Send(address, subject, body);
        }
    }
}
