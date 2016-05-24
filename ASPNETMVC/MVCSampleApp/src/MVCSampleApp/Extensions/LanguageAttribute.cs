using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCSampleApp.Extensions
{
    public class LanguageAttribute : ActionFilterAttribute
    {
        private string _language = null;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _language = filterContext.RouteData.Values["language"]?.ToString();

            //...
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }
    }

}
