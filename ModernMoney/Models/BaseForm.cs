using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ModernMoney.Models
{
    public class BaseForm:TableEntity
    {
        public BaseForm()
        {
            PartitionKey = Guid.NewGuid().ToString();
            RowKey = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss.fff");
        }

        public string Dummy { get; set; }
        public ConversationType Type { get; set; }
        public string ConversationTypeDesc { get; set; }
        public string Source { get; set; } = "Modern Money";
    }
}
