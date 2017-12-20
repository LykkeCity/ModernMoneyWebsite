using AzureRepositories.Conversation;
using System.ComponentModel.DataAnnotations;

namespace ModernMoney.Models
{
    public class NewsletterModel : BaseForm
    {
        public NewsletterModel()
        {
            ConversationTypeDesc = ConversationType.Newsletter.ToString();
        }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        public ConversationEntity Create(NewsletterModel model)
        {
            return new ConversationEntity()
            {
                Email = model.Email,
                ConversationTypeDesc = ConversationType.Newsletter.ToString(),
            };
        }
    }
}
