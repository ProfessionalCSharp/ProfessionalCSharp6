using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using static System.Console;

namespace XmlReaderSample
{
    public class Program
    {
        public void Main(string[] args)
        {


        }

        void Sample1()
        {
            XmlReader reader = XmlReader.Create("books.xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    WriteLine(reader.Value);
                }
            }
        }

        public void Sample2()
        {
            XmlReader reader = XmlReader.Create("books.xml");
            while (!reader.EOF)
            {
                //if we hit an element type, try and load it in the listbox
                if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == "title")
                {
                    WriteLine(reader.ReadElementContentAsString());
                }
                else
                {
                    //otherwise move on
                    reader.Read();
                }
            }

        }

        public void Sample3()
        {
            XmlReader reader = XmlReader.Create("books.xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "price")
                    {
                        decimal price = reader.ReadElementContentAsDecimal();
                        WriteLine($"Current Price = {price}");
                        price += price * (decimal).25;
                        WriteLine($"New price {price}");
                    }
                    else if (reader.Name == "title")
                    {
                        WriteLine(reader.ReadContentAsString());
                    }
                }
            }

        }

        public void AttributeSample()
        {
            XmlReader reader = XmlReader.Create("books.xml");
            //Read in node at a time
            while (reader.Read())
            {
                //check to see if it's a NodeType element
                if (reader.NodeType == XmlNodeType.Element)
                {
                    //if it's an element, then let's look at the attributes.
                    for (int i = 0; i < reader.AttributeCount; i++)
                    {
                        WriteLine(reader.GetAttribute(i));
                    }
                }
            }

        }

        public void ValidatingSample()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, "books.xsd");
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler +=
              new System.Xml.Schema.ValidationEventHandler(settings_ValidationEventHandler);
            XmlReader reader = XmlReader.Create("books.xml", settings);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    WriteLine(reader.Value);
                }
            }

        }

        public void WriterSample()
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            StreamWriter stream = File.CreateText("newbook.xml");
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteStartDocument();
                //Start creating elements and attributes
                writer.WriteStartElement("book");
                writer.WriteAttributeString("genre", "Mystery");
                writer.WriteAttributeString("publicationdate", "2001");
                writer.WriteAttributeString("ISBN", "123456789");
                writer.WriteElementString("title", "Case of the Missing Cookie");
                writer.WriteStartElement("author");
                writer.WriteElementString("name", "Cookie Monster");
                writer.WriteEndElement();
                writer.WriteElementString("price", "9.99");
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }
    }
}
