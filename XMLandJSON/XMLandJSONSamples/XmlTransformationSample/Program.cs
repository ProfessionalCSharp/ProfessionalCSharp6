using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlTransformationSample
{
    public class Program
    {
        public void Main(string[] args)
        {
        }

        public void Sample1()
        {
            var trans = new XslCompiledTransform();
            trans.Load("books.xsl");
            trans.Transform("books.xml", "out.html");
            webBrowser1.Navigate(AppDomain.CurrentDomain.BaseDirectory + "out.html");

        }
    }
}
