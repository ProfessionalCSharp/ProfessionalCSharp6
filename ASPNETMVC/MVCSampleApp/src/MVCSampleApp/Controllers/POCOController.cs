using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVCSampleApp.Controllers
{
    public class POCOController
    {
        public string Index() =>
          "this is a POCO controller";

        [ActionContext]
        public ActionContext ActionContext { get; set; }
        public HttpContext Context => ActionContext.HttpContext;
        public ModelStateDictionary ModelState => ActionContext.ModelState;

        public string UserAgentInfo()
        {
            if (Context.Request.Headers.ContainsKey("User-Agent"))
            {
                return Context.Request.Headers["User-Agent"];
            }
            return "No user-agent information";
        }
    }

}
