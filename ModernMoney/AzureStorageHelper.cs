using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace CryptoBank
{
    public static class AzureStorageHelper
    {
        public static void Store(ITableEntity conversation)
        {
            CloudTableClient tableClient = new CloudTableClient(
               new Uri(ApplicationSettings.AppSettings.CryptoBankWebsite.AzureStorage.Uri),
               new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                   ApplicationSettings.AppSettings.CryptoBankWebsite.AzureStorage.AccountName,
                   ApplicationSettings.AppSettings.CryptoBankWebsite.AzureStorage.AccountKey));

            CloudTable table = tableClient.GetTableReference("Conversation");
            table.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.Insert(conversation);

            table.ExecuteAsync(insertOperation);
        }
    }
}
