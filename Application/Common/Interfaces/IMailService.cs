using System.Threading.Tasks;
using SendGrid;
namespace Application.Common.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string content, string fromMail);

        Task<Response> SendNotificationEmailAsync(string toEmail, string subject, string content, string fromMail);
    }
}
