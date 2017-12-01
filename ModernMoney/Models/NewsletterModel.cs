using System.ComponentModel.DataAnnotations;

namespace ModernMoney.Models
{
    public class NewsletterModel:BaseForm
    {
        public NewsletterModel()
        {
            ConversationTypeDesc = ConversationType.Newsletter.ToString();
        }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }
    }
}
