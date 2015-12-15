namespace WebSampleApp
{
    public static class HtmlExtensions
    {
        public static string Div(this string value) =>
            $"<div>{value}</div>";

        public static string Span(this string value) =>
            $"<span>{value}</span>";
    }
}
