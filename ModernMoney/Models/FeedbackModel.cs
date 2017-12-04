using System.ComponentModel.DataAnnotations;

namespace ModernMoney.Models
{
    public class FeedbackModel:BaseForm
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
        
    }
}
