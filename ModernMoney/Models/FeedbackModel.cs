using AzureRepositories.Conversation;
using System.ComponentModel.DataAnnotations;

namespace ModernMoney.Models
{
    public class FeedbackModel : BaseForm
    {
        public FeedbackModel()
        {
            ConversationTypeDesc = ConversationType.Feedback.ToString();
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }
        public string Message { get; set; }

        public ConversationEntity Create(FeedbackModel model)
        {
            return new ConversationEntity()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Message = model.Message,
                ConversationTypeDesc = ConversationType.Feedback.ToString(),
            };
        }
    }
}
