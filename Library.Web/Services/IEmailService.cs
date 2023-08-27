namespace Library.Web.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string recipientEmail,  string subject, string body);
    }
}
