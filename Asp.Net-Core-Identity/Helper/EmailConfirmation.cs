using System.Net.Mail;

namespace Asp.Net_Core_Identity.Helper
{
    public static class EmailConfirmation
    {
        public static void SendEmail(string link, string email)

        {
            MailMessage mail = new();

            SmtpClient smtpClient = new("smtp adresi");

            mail.From = new MailAddress("mail adresi");
            mail.To.Add(email);

            mail.Subject = $"www.bıdıbı.com::Email doğrulama";
            mail.Body = "<h2>Email adresinizi doğrulamak için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>email doğrulama linki</a>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("mail adresi", "Şifre");

            smtpClient.Send(mail);
        }
    }
}
