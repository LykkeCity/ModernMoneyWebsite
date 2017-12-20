using AzureRepositories.Conversation;
using System.ComponentModel.DataAnnotations;

namespace ModernMoney.Models
{
    public class BetaModel : BaseForm
    {
        public BetaModel()
        {
            ConversationTypeDesc = ConversationType.JoinBeta.ToString();
        }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        public ConversationEntity Create(BetaModel model)
        {
            return new ConversationEntity()
            {
                Email = model.Email,
                ConversationTypeDesc = ConversationType.JoinBeta.ToString(),
            };
        }
    }
}
