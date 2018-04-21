using System;
using System.IO;
using System.Threading.Tasks;
using KiraNet.Camellia.AuthorizationServer.Common;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace KiraNet.Camellia.AuthorizationServer.Serivces
{
    public class EmailSender : IEmailSender
    {
        private string _tplPath;
        private string _fromName;
        private string _fromAddress;
        private string _token;

        public EmailSender(IConfiguration Configuration)
        {
            _tplPath = Configuration["EmailSettings:EmailTplPath"];
            _fromName = Configuration["EmailSettings:FromName"];
            _fromAddress = Configuration["EmailSettings:FromAddress"];
            _token = Configuration["EmailSettings:Token"];
        }

        public async Task SendEmailAsync(string email, string username, string subject, string message, EmailTpl emailTpl)
        {
            var content = await GetHtmlTpl(emailTpl);
            if (String.IsNullOrWhiteSpace(content))
            {
                return;
            }
            content = content.Replace("{host}", _fromName)
                             .Replace("{name}", username)
                             .Replace("{subject}", subject)
                             .Replace("{content}", message);
            //设置基本信息
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_fromName, _fromAddress));
            mimeMessage.To.Add(new MailboxAddress(subject, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = content
            };

            //链接发送
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                //采用qq邮箱服务器发送邮件
                client.Connect("smtp.qq.com", 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                //qq邮箱，密码(安全设置短信获取后的密码)
                client.Authenticate("kirayoshikage@qq.com", "gsbddnoexqdmbegb");

                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }



        private async Task<string> GetHtmlTpl(EmailTpl tpl)
        {
            var separator = Path.DirectorySeparatorChar;
            if (_tplPath == null)
            {
                _tplPath = separator + "wwwroot" + separator + "tpl";
            }

            var content = string.Empty;
            if (string.IsNullOrWhiteSpace(_tplPath))
            {
                return content;
            }

            var folderPath = Directory.GetCurrentDirectory() + separator + _tplPath;
            var path = $"{folderPath}{separator}{tpl}.html";
            try
            {
                using (var stream = File.OpenRead(path))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = await reader.ReadToEndAsync(); // 读取HTML模板
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return content;
        }
    }
}
