using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSampleApp.Helpers
{
    public static class HtmlHelper
    {
        public static string DocType() => "<!DOCTYPE HTML>";

        public static string Head() => "<head><meta charset=\"utf-8\"><title>Sample</title></head>";

        public static string HtmlStart() => "<html lang=\"en\">";
        public static string HtmlEnd() => "</html>";
        public static string BodyStart() => "<body>";
        public static  string BodyEnd() => "</body>";
    }
}
