using System;
using System.Collections.Generic;
using System.Net;

namespace Wrox.ProCSharp.Async
{
    public interface IImageRequest
    {
        string SearchTerm { get; set; }
        string Url { get; }

        KeyValuePair<string, string>[] Headers { get; }

        IEnumerable<SearchItemResult> Parse(string xml);

        ICredentials Credentials { get; }
    }
}
