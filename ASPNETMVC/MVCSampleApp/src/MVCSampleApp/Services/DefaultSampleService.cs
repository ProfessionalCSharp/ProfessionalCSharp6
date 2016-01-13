using System.Collections.Generic;

namespace MVCSampleApp.Services
{
    public class DefaultSampleService : ISampleService
    {
        private List<string> _strings = new List<string> { "one", "two", "three" };
        public IEnumerable<string> GetSampleStrings() => _strings;
    }
}
