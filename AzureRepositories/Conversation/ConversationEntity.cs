using Core.Conversation;
using Core.Enumerations;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AzureRepositories.Conversation
{
    public class ConversationEntity : TableEntity, IConversation
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


        public static string GeneratePartition()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GenerateRowKey()
        {
            return DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss.fff");
        }

        public static string SetSource()
        {
            return "Modern Money";
        }

        public static ConversationEntity Create(IConversation conversation)
        {
            return new ConversationEntity
            {
                PartitionKey = GeneratePartition(),
                RowKey = GenerateRowKey(),

                Email = conversation.Email,
                FullName = conversation.FullName,
                FirstName = conversation.FirstName,
                LastName = conversation.LastName,
                PhoneNumber = conversation.PhoneNumber,
                Message = conversation.Message,
                Type = conversation.Type,
                Dummy = conversation.Dummy,
                ConversationTypeDesc = conversation.ConversationTypeDesc,
                Source = SetSource()
            };
        }
    }
}
