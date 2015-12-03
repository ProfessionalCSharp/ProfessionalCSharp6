using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Configuration;

namespace SaasWebHooksReceiverSample.WebHookHandlers
{
    public class QueueManager
    {
        private CloudStorageAccount _storageAccount;

        public QueueManager()
        {
            _storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);
        }

        public void WriteToQueueStorage(string queueName, string actions, string json)
        {
            CloudQueueClient client = _storageAccount.CreateCloudQueueClient();

            CloudQueue queue = client.GetQueueReference(queueName);
            queue.CreateIfNotExists();

            var message = new CloudQueueMessage(actions + "---" + json);
            queue.AddMessage(message);

        }
    }
}
