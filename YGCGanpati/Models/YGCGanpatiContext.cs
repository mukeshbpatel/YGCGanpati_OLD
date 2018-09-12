using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YGCGanpati.Models
{    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<Sponser> Sponsers { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }        
        public System.Data.Entity.DbSet<YGCGanpati.Models.Event> Events { get; set; }
    }

    public static class Common
    {
        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }       

        public static string FormatAmount(object Amount)
        {
            if (Amount == null)
                return string.Empty;
            else
                return string.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:#,#}", Amount);
        }
    }

    public class YGCEmail
    {

        private string MailBody = @"<table style='table-layout: fixed; border:3px ridge darkgray; width:450px; font-family:Verdana,Arial; font-size:small; margin: 10px 10px 10px 10px;' cellspacing='4' cellpadding='4'>
    <tr>
        <td style='white-space: nowrap; font-size:x-large; text-align:left; vertical-align:top; font-weight:bold;' colspan='4' rowspan='2'>Cash Receipt</td>
        <td style='white-space: nowrap; text-align:right;' colspan='2'>Receipt No. : <b>{Number}</b></td>
    </tr>
    <tr>
        <td style='white-space: nowrap; text-align:right;' colspan='2'>Date :  <b>{Date}</b></td>
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap; padding-top:10px;'>
            Received with thanks from <b>{Name}</b>
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap;'>
            the sum of <b>Rs. {Amount}/- ({AmountWord})<b>
        </td>
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap;'>
         by Cash for the month of <b>{Month}</b> contribution
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap; '>
            towards <b>Yashraj Green Castle Fund.</b>
        </td>        
    </tr>
     <tr>
        <td colspan='6' style='padding-top:30px; text-align:right;'>
            Received By,            
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='text-align:right; font-weight:bold; font-style: italic;'>
            YGC Fund Treasurer
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='padding-top:15px; text-align:center; font-weight:bold; font-style: italic;'>
           <a href='http://www.ygconline.com'>http://www.ygconline.com</a>
        </td>        
    </tr>
    </table>";

        private string EMIMailBody = @"<table style='table-layout: fixed; border:3px ridge darkgray; width:500px; font-family:Verdana,Arial; font-size:small; margin: 10px 10px 10px 10px;' cellspacing='4' cellpadding='4'>
    <tr>
        <td style='white-space: nowrap; font-size:x-large; text-align:left; vertical-align:top; font-weight:bold;' colspan='4' rowspan='2'>EMI Receipt</td>
        <td style='white-space: nowrap; text-align:right;'>Receipt No. : </td>
        <td style='white-space: nowrap; border-bottom:1px solid darkgray; padding-left:10px;'>{Number}</td>
    </tr>
    <tr>
        <td style='white-space: nowrap; text-align:right;'>EMI Date : </td>
        <td style='white-space: nowrap; border-bottom:1px solid darkgray; padding-left:10px;'>{Date}</td>
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap; padding-top:10px;'>
            Received with thanks from <b>{Name}</b>
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap;'>
            the sum of <b>Rs. {Amount}/- ({AmountWord})<b>
        </td>
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap;'>
         by Cash for Loan EMI of the month <b>{Month}</b>
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='white-space: nowrap; '>
            towards <b>Yashraj Green Castle Fund.</b>
        </td>        
    </tr>
     <tr>
        <td colspan='6' style='padding-top:30px; text-align:right;'>
            Received By,            
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='text-align:right; font-weight:bold; font-style: italic;'>
            YGC Fund Treasurer
        </td>        
    </tr>
    <tr>
        <td colspan='6' style='padding-top:15px; text-align:center; font-weight:bold; font-style: italic;'>
           <a href='http://www.ygconline.com'>http://www.ygconline.com</a>
        </td>        
    </tr>
    </table>";
       

        public Boolean SendPasswordResetMail(ApplicationUser userprofile)
        {
            bool result = false;
            if (String.IsNullOrEmpty(userprofile.Email))
            {
                return false;
            }
            try
            {

                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = "smtp.mail.yahoo.com";
                client.Port = 587;
                // setup Smtp authentication
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("ygcfund@yahoo.com", "y@cfund!@#$");
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("ygcfund@yahoo.com", "Yashraj Green Castle");
                msg.To.Add(new MailAddress(userprofile.Email, userprofile.Name));
                msg.Bcc.Add(new MailAddress("mukeshbpatel@gmail.com", userprofile.Name));
                msg.Subject = "Password Reset";
                msg.IsBodyHtml = true;
                msg.Body = "<b>Hi " + userprofile.Name + "</b>, <br/><br/>Your password is rest for Yashraj Green Castle account.<br/>You can use following login details to access your account.<br/><br/>Mobile: <b>" + userprofile.UserName + "</b><br/>Password: <b>1234</b><br/><br/>Thanks and Regards,<br/><b>YGC Admin</b><br/><br/>";
                client.Send(msg);
                result = true;
                msg = null;
                client = null;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        
        private string NumbersToWords(decimal inputNumber)
        {
            int inputNo = (int)inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
    }
}