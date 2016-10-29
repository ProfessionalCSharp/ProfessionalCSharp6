using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Wrox.ProCSharp.Async
{
    public class FlickrRequest : IImageRequest
    {
        private const string AppId = "enter your Flickr app-id here";

        public FlickrRequest()
        {
            Count = 50;
            Page = 1;
        }
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { _searchTerm = value; }
        }

        public string Url => $"https://api.flickr.com/services/rest?api_key={AppId}&method=flickr.photos.search&content_type=1&text={SearchTerm}&per_page={Count}&page={Page}";

        public KeyValuePair<string, string>[] Headers => new KeyValuePair<string, string>[0];

        public int Count { get; set; }
        public int Page { get; set; }

        public IEnumerable<SearchItemResult> Parse(string xml)
        {
            XElement respXml = XElement.Parse(xml);
            return (from item in respXml.Descendants("photo")
                    select new SearchItemResult
                    {
                        Title = new string(item.Attribute("title").Value.Take(50).ToArray()),
                        Url = string.Format("http://farm{0}.staticflickr.com/{1}/{2}_{3}_z.jpg",
                        item.Attribute("farm").Value, item.Attribute("server").Value, item.Attribute("id").Value, item.Attribute("secret").Value),
                        ThumbnailUrl = string.Format("http://farm{0}.staticflickr.com/{1}/{2}_{3}_t.jpg",
                        item.Attribute("farm").Value, item.Attribute("server").Value, item.Attribute("id").Value, item.Attribute("secret").Value),
                        Source = "Flickr"
                    }).ToList();
        }

        public ICredentials Credentials => null;

    }
}
