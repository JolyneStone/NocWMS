using System.Text.Encodings.Web;
using System.Threading.Tasks;
using KiraNet.Camellia.AuthorizationServer.Common;
using KiraNet.Camellia.AuthorizationServer.Serivces;

namespace KiraNet.Camellia.AuthorizationServer.Extensions
{
    public static class EmailExtensions
    {
        public static async Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string username, string link)
        {
            await emailSender.SendEmailAsync(email, username, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>",
                EmailTpl.SettingEmail);
        }

        public static async Task SendEmailResetPasswordAsync(this IEmailSender emailSender, string email, string username, string link)
        {
            await emailSender.SendEmailAsync(email, username, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>",
                   EmailTpl.MsgBox);
        }
    }
}
