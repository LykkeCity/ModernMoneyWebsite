using Core.Enumerations;

namespace Core.Conversation
{
    public class Conversation : IConversation
    {
        public string Email { get; set; }

        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }

        public ConversationType Type { get; set; }
        public string Dummy { get; set; }
        public string ConversationTypeDesc { get; set; }
        public string Source { get; set; }
    }
}
