using System;
using System.Collections.Generic;
using System.Net;

namespace Wrox.ProCSharp.Async
{
  public interface IImageRequest
  {
    string SearchTerm { get; set; }
    string Url { get; }

    IEnumerable<SearchItemResult> Parse(string xml);

    ICredentials Credentials { get; }
  }
}
