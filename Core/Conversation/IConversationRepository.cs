using System.Threading.Tasks;

namespace Core.Conversation
{
    public interface IConversationRepository
    {
        Task CreateAsync(IConversation conversation);
    }
}
