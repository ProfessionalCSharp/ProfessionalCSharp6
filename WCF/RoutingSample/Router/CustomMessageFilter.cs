using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Router
{
    public class CustomMessageFilter : MessageFilter
    {
        private string _filterParam;
        public CustomMessageFilter(string filterParam)
        {
            _filterParam = filterParam;
        }

        public override bool Match(Message message) => true;

        private XPathExpression _filterExpression = null;

        public override bool Match(MessageBuffer buffer)
        {
            if (_filterExpression == null)
            {
                _filterExpression = XPathExpression.Compile($"////value == {_filterParam}");
            }

            XPathNavigator navigator = buffer.CreateNavigator();
            return navigator.Matches(_filterExpression);
            //XDocument doc = await GetMessageEnvelope(buffer);
            //return Match(doc);
        }

        private bool Match(XDocument doc)
        {
            string result = doc.Elements("value").Where(x => x.Value == "HelloA").SingleOrDefault().Value;
            return result != null;
        }

        private async Task<XDocument> GetMessageEnvelope(MessageBuffer buffer)
        {
            using (var stream = new MemoryStream())
            {
                var msg = buffer.CreateMessage();
                XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream);
                msg.WriteMessage(writer);
                await writer.FlushAsync();
                stream.Seek(0, SeekOrigin.Begin);
                XDocument doc = XDocument.Load(stream);
                return doc;

            }
        }
    }
}
