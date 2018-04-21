using System.Threading.Tasks;
using KiraNet.Camellia.AuthorizationServer.Common;

namespace KiraNet.Camellia.AuthorizationServer.Serivces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email,string username, string subject, string message, EmailTpl emailTpl);
    }
}
