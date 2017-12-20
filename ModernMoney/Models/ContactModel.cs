using AzureRepositories.Conversation;
using System.ComponentModel.DataAnnotations;

namespace ModernMoney.Models
{
    public class ContactModel : BaseForm
    {
        public ContactModel()
        {
            ConversationTypeDesc = ConversationType.Contact.ToString();
        }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }
        public string Message { get; set; }

        public ConversationEntity Create(ContactModel model)
        {
            return new ConversationEntity()
            {
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Message = model.Message,
                ConversationTypeDesc = ConversationType.Contact.ToString(),
            };
        }
    }
}
