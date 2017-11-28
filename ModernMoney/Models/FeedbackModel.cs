using System.ComponentModel.DataAnnotations;

namespace CryptoBank.Models
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
        [EmailAddress(ErrorMessage = "Please enter valid e-mail address")]
        public string Email { get; set; }
        public string Message { get; set; }
        
    }
}
