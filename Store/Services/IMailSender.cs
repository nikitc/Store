namespace Store.Services
{
    public interface IMailSender
    {
        bool Send(string toAddress, string subject, string body);
        bool SendStandardEmailConfirm(string address, string token);
    }
}
