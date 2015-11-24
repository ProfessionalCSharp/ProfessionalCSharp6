using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Wrox.ProCSharp.Async
{
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

        public string Url => $"https://api.datamarket.azure.com/Data.ashx/Bing/Search/v1/Image?Query=%27{SearchTerm}%27&$top={Count}&$skip={Offset}&$format=Atom";



        public int Count { get; set; }
        public int Offset { get; set; }

        public IEnumerable<SearchItemResult> Parse(string xml)
        {
            XElement respXml = XElement.Parse(xml);
            XNamespace d = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices");
            XNamespace m = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");

            return (from item in respXml.Descendants(m + "properties")
                    select new SearchItemResult
                    {
                        Title = new string(item.Element(d + "Title").Value.Take(50).ToArray()),
                        Url = item.Element(d + "MediaUrl").Value,
                        ThumbnailUrl = item.Element(d + "Thumbnail").Element(d + "MediaUrl").Value,
                        Source = "Bing"
                    }).ToList();
        }


        public ICredentials Credentials => new NetworkCredential(AppId, AppId);

    }
}
