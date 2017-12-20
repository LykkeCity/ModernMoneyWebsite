using AzureRepositories.Conversation;
using AzureStorage.Tables;
using Common.Log;
using Lykke.SettingsReader;

namespace AzureRepositories
{
    public class AzureRepoBinder
    {
        public static ConversationRepository CreateConversationInformationRepository(IReloadingManager<string> connString, ILog log)
        {
            return new ConversationRepository(AzureTableStorage<ConversationEntity>.Create(connString, "Conversation", log));
        }
    }
}
