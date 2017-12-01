using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ModernMoney.Models;
using System;
using System.Net;
using System.Net.Mail;
using ModernMoney.Infrastructure;
using Lykke.SettingsReader;

namespace ModernMoney
{
    public static class EmailSender
    {

        public static IConfigurationRoot Configuration { get; set; }

        private static SmtpClient CreateMailClient()
        {
            var client = new SmtpClient();

            client.UseDefaultCredentials = ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.UseDefaultCredentials;
            client.Host = ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Host;
            client.Port = ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Port;
            client.EnableSsl = ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.EnableSsl;

            client.Credentials =
                new NetworkCredential(
                    ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Credentials.Username,
                    ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Credentials.Password);

            return client;
        }

        static EmailSender() {
        }

        public static void SendFeedback(FeedbackModel feedback) {

            CreateMailClient().Send(
               ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Credentials.Username,
               ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.FeedbackRecipient,
               ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.FeedbackSubject, 
                feedback.Message);
        }

        public static void SendContact(ContactModel contact)
        {

            CreateMailClient().Send(
               ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Credentials.Username,
               ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.FeedbackRecipient,
               ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.FeedbackSubject,
                contact.Message);
        }

        public static void SendBeta(IHostingEnvironment env, BetaModel beta)
        {
            MailAddress from = new MailAddress(ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.Credentials.Username, "");
            MailAddress to = new MailAddress(beta.Email, "");

            MailMessage message = new MailMessage(from, to);
           
            message.IsBodyHtml = true;
            message.Subject = ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.JoinBetaSubject;

            message.Body = FileHelper.Load(env, ApplicationSettings.AppSettings.ModernMoneyWebsite.Email.TemplatesFolder, "email-join-beta.html");

            CreateMailClient().Send(message);

        }
    }
}
