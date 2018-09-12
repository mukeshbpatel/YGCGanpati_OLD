using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace YGCGanpati.Models
{
    public class EmailNotification
    {

        public Boolean SendMail(MailMessage mailMessage)
        {
            bool result = false;
            try
            {
                if (true)
                {
                    mailMessage.Bcc.Add(new MailAddress("yashrajgreencastle@outlook.com"));
                    SmtpClient client = new SmtpClient();
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Host = "smtp.mail.yahoo.com";
                    client.Port = 587;
                    // setup Smtp authentication
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("yashrajgreencastle@yahoo.com", "YGC@Kalepadal");
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;
                    client.Send(mailMessage);
                    client.Dispose();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        private MailMessage CreateMessage()
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("yashrajgreencastle@yahoo.com", "Yashraj Green Castle");
            msg.IsBodyHtml = true;
            return msg;
        }

        public bool SendWelcomeEmail(ApplicationUser user, string Password)
        {
            string Body, Subject;
            Subject = "Welcome to YGC Digital Platform";
            Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Template/Welcome.html"));
            Body = Body.Replace("{{Name}}", user.Name)
                .Replace("{{UserID}}", user.Name)
                .Replace("{{Password}}", Password);
            MailMessage msg = CreateMessage();
            if (!String.IsNullOrEmpty(user.Email))
            {
                msg.To.Add(new MailAddress(user.Email, user.Name));
            }
            msg.Subject = Subject;
            msg.Body = Body;
            return SendMail(msg);
        }

        public bool SendForgetPasswordEmail(ApplicationUser user, string Password)
        {
            string Body, Subject;
            Subject = "Reset Password";
            Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Template/ForgetPassword.html"));
            Body = Body.Replace("{{Name}}", user.Name)
                .Replace("{{UserID}}", user.Name)
                .Replace("{{Password}}", Password);                
            MailMessage msg = CreateMessage();
            if (!String.IsNullOrEmpty(user.Email))
            {
                msg.To.Add(new MailAddress(user.Email, user.Name));
            }
            msg.Subject = Subject;
            msg.Body = Body;
            return SendMail(msg);
        }

    }
}