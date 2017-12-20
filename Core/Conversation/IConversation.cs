using Core.Enumerations;

namespace Core.Conversation
{
    public interface IConversation
    {
        string Email { get; set; }

        string FullName { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }

        string PhoneNumber { get; set; }
        string Message { get; set; }

        ConversationType Type { get; set; }
        string Dummy { get; set; }
        string ConversationTypeDesc { get; set; }
        string Source { get; set; }
    }
}
