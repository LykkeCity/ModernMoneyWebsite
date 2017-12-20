namespace ModernMoney.Models
{
    public class BaseForm
    {
        public string Dummy { get; set; }
        public ConversationType Type { get; set; }
        public string ConversationTypeDesc { get; set; }
        public string Source { get; set; } = "Modern Money";
    }
}
