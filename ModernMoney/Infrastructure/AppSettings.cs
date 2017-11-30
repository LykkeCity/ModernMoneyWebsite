namespace ModernMoney.Infrastructure
{
    public class AppSettings
    {
        public ModernMoneyWebsite ModernMoneyWebsite {get;set;}
    }

    public class ModernMoneyWebsite
    {
        public AzureStorage AzureStorage { get; set; }
        public Email Email { get; set; }
    }

    public class AzureStorage
    {
        public string Uri { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
    }

    public class Email
    {
        public string TemplatesFolder { get; set; }
        public string FeedbackRecipient { get; set; }
        public string FeedbackSubject { get; set; }
        public string JoinBetaSubject { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public Credentials Credentials { get; set; }
        public Messages Messages { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Messages
    {
        public string Feedback { get; set; }
        public string Newsletter { get; set; }
        public string Beta { get; set; }
    }
}
