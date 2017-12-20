namespace Core.Settings
{
    public class EmailSettings
    {
        public string TemplatesFolder { get; set; }
        public string FeedbackRecipient { get; set; }
        public string FeedbackSubject { get; set; }
        public string JoinBetaSubject { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public CredentialsSettings Credentials { get; set; }
        public MessagesSettings Messages { get; set; }
    }
}
