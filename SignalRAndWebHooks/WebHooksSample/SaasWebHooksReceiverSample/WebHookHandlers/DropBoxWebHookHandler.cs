using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace SaasWebHooksReceiverSample.WebHookHandlers
{
    public class DropboxWebHookHandler : WebHookHandler
    {
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            if ("Dropbox".Equals(receiver, StringComparison.CurrentCultureIgnoreCase))
            {
                QueueManager queue = null;
                try
                {
                    queue = new QueueManager();

                    string actions = string.Join(", ", context.Actions);
                    JObject incoming = context.GetDataOrDefault<JObject>();

                    queue.WriteToQueueStorage("dropboxqueue", actions, incoming.ToString());
                }
                catch (Exception ex)
                {
                    queue?.WriteToQueueStorage("dropboxqueue", "error", ex.Message);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}
