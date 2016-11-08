using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Wrox.ProCSharp.Async
{
    public class BingImage
    {
        public string Name { get; set; }
        public string ContentUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
    public class BingResult
    {
        public IEnumerable<BingImage> Value { get; set; }
    }

    public class BingRequest : IImageRequest
    {
        private const string AppId = "enter your BING key here";

        public BingRequest()
        {
            Count = 50;
            Offset = 0;
        }
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { _searchTerm = value; }
        }

        public string Url => $"https://api.cognitive.microsoft.com/bing/v5.0/images/search?q={SearchTerm}&count={Count}&offset={Offset}";

        public KeyValuePair<string, string>[] Headers =>
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("Ocp-Apim-Subscription-Key", AppId),
                new KeyValuePair<string, string>("User-Agent", "Professional C# Sample App")
            };

        public int Count { get; set; }
        public int Offset { get; set; }

        public IEnumerable<SearchItemResult> Parse(string json)
        {
            BingResult imageResults = JsonConvert.DeserializeObject<BingResult>(json);
            return imageResults.Value.Select(r => new SearchItemResult
            {
                Title = r.Name,
                Url = r.ContentUrl,
                ThumbnailUrl = r.ThumbnailUrl,
                Source = "Bing"
            }).ToList();
        }


        public ICredentials Credentials => new NetworkCredential(AppId, AppId);
    }
}
