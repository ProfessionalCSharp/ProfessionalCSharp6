using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCSampleApp.Extensions
{
    // You may need to install the Microsoft.AspNet.Razor.Runtime package into your project
    [HtmlTargetElement("div")]
    [HtmlTargetElement("input")]
    public class DateTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

        }
    }
}
