using System.IO;
using System.Xml;
using System.Xml.XPath;
using static System.Console;

namespace XPathNavigatorSample
{
    class Program
    {
        private const string BooksFileName = "books.xml";
        private const string NewBooksFileName = "newbooks.xml";
        private const string NavigateOption = "-n";
        private const string EvaluateOption = "-e";
        private const string ChangeOption = "-c";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            switch (args[0])
            {
                case NavigateOption:
                    SimpleNavigate();
                    break;
                case EvaluateOption:
                    UseEvaluate();
                    break;
                case ChangeOption:
                    Insert();
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        private static void ShowUsage()
        {
            WriteLine("XmlReaderAndWriterSample options");
            WriteLine("\tOptions");
            WriteLine($"\t{NavigateOption}\tNavigate");
            WriteLine($"\t{EvaluateOption}\tEvaluate");
            WriteLine($"\t{ChangeOption}\tChange");
        }

        public static void SimpleNavigate()
        {
            //modify to match your path structure
            var doc = new XPathDocument(BooksFileName);
            //create the XPath navigator
            XPathNavigator nav = doc.CreateNavigator();
            //create the XPathNodeIterator of book nodes
            // that have genre attribute value of novel
            XPathNodeIterator iterator = nav.Select("/bookstore/book[@genre='novel']");

            while (iterator.MoveNext())
            {
                XPathNodeIterator newIterator = iterator.Current.SelectDescendants(XPathNodeType.Element, matchSelf: false);
                while (newIterator.MoveNext())
                {
                    WriteLine($"{newIterator.Current.Name}: {newIterator.Current.Value}");
                }
            }
        }

        public static void UseEvaluate()
        {
            //modify to match your path structure
            var doc = new XPathDocument(BooksFileName);
            //create the XPath navigator
            XPathNavigator nav = doc.CreateNavigator();
            //create the XPathNodeIterator of book nodes
            XPathNodeIterator iterator = nav.Select("/bookstore/book");
            while (iterator.MoveNext())
            {
                if (iterator.Current.MoveToChild("title", string.Empty))
                {
                    WriteLine($"{iterator.Current.Name}: {iterator.Current.Value}");
                }
            }
            WriteLine("=========================");
            WriteLine($"Total Cost = {nav.Evaluate("sum(/bookstore/book/price)")}");
        }

        public static void Insert()
        {
#if NET46
            var doc = new XmlDocument();
            doc.Load(BooksFileName);
#else
            var doc = new XPathDocument(BooksFileName);
#endif

            XPathNavigator navigator = doc.CreateNavigator();

            if (navigator.CanEdit)
            {
                WriteLine($"edit {NewBooksFileName} and add <disc>");
                XPathNodeIterator iter = navigator.Select("/bookstore/book/price");

                while (iter.MoveNext())
                {
                    iter.Current.InsertAfter("<disc>5</disc>");
                }
            }          

            using (var stream = File.CreateText(NewBooksFileName))
            {
                var outDoc = new XmlDocument();
                outDoc.LoadXml(navigator.OuterXml);
                outDoc.Save(stream);
            }
        }
    }
}
