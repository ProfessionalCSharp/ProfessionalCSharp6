using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
