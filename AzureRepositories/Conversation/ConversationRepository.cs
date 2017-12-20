using AzureStorage;
using Core.Conversation;
using System.Threading.Tasks;

namespace AzureRepositories.Conversation
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly INoSQLTableStorage<ConversationEntity> _tableStorage;

        public ConversationRepository(INoSQLTableStorage<ConversationEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public Task CreateAsync(IConversation conversation)
        {
            var entity = ConversationEntity.Create(conversation);
            return _tableStorage.InsertAsync(entity);
        }
    }
}
