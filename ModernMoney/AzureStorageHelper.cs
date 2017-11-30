using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ModernMoney
{
    public static class AzureStorageHelper
    {
        public static void Store(ITableEntity conversation)
        {
            CloudTableClient tableClient = new CloudTableClient(
               new Uri(ApplicationSettings.AppSettings.ModernMoneyWebsite.AzureStorage.Uri),
               new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                   ApplicationSettings.AppSettings.ModernMoneyWebsite.AzureStorage.AccountName,
                   ApplicationSettings.AppSettings.ModernMoneyWebsite.AzureStorage.AccountKey));

            CloudTable table = tableClient.GetTableReference("Conversation");
            table.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.Insert(conversation);

            table.ExecuteAsync(insertOperation);
        }
    }
}
