using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace SaasWebHooksReceiverSample.WebHookHandlers
{
    public class GithubWebHookHandler : WebHookHandler
    {
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            if ("GitHub".Equals(receiver, StringComparison.CurrentCultureIgnoreCase))
            {
                QueueManager queue = null;
                try
                {
                    queue = new QueueManager();
                    string actions = string.Join(", ", context.Actions);
                    JObject incoming = context.GetDataOrDefault<JObject>();

                    queue.WriteToQueueStorage("githubqueue", actions, incoming.ToString());
                }
                catch (Exception ex)
                {
                    queue?.WriteToQueueStorage("githubqueue", "error", ex.Message);
                }
            }
            return Task.FromResult<object>(null);
        }
    }
}
