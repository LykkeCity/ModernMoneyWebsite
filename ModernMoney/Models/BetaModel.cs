using System.ComponentModel.DataAnnotations;

namespace CryptoBank.Models
{
    public class BetaModel:BaseForm
    {
        public BetaModel()
        {
            ConversationTypeDesc = ConversationType.JoinBeta.ToString();
        }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid e-mail address")]
        public string Email { get; set; }
    }
}
