using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WhatsNewAttributes;
using static System.Console;

namespace LookupWhatsNew
{
    class Program
    {
        private static readonly StringBuilder outputText = new StringBuilder(1000);
        private static DateTime backDateTo = new DateTime(2015, 2, 1);

        static void Main()
        {
            Assembly theAssembly = Assembly.Load(new AssemblyName("VectorClass"));
            Attribute supportsAttribute = theAssembly.GetCustomAttribute(typeof(SupportsWhatsNewAttribute));
            string name = theAssembly.FullName;

            AddToOutput($"Assembly: {name}");

            if (supportsAttribute == null)
            {
                AddToOutput("This assembly does not support WhatsNew attributes");
                return;
            }
            else
            {
                AddToOutput("Defined Types:");
            }
            IEnumerable<Type> types = theAssembly.ExportedTypes;

            foreach (Type definedType in types)
            {
                DisplayTypeInfo(definedType);
            }

            WriteLine($"What\'s New since {backDateTo:D}");
            WriteLine(outputText.ToString());

            ReadLine();
        }

        static void AddToOutput(string Text)
        {
            outputText.Append("\n" + Text);
        }

        private static void DisplayTypeInfo(Type type)
        {
            if (!type.GetTypeInfo().IsClass)
            {
                return;
            }

            AddToOutput($"\nclass {type.Name}");

            IEnumerable<LastModifiedAttribute> attributes = type.GetTypeInfo().GetCustomAttributes().OfType<LastModifiedAttribute>();
            if (attributes.Count() == 0)
            {
                AddToOutput("No changes to this class\n");
            }
            else
            {
                foreach (LastModifiedAttribute attribute in attributes)
                {
                    WriteAttributeInfo(attribute);
                }
            }

            AddToOutput("changes to methods of this class:");

            foreach (MethodInfo method in type.GetTypeInfo().DeclaredMembers.OfType<MethodInfo>())
            {
                IEnumerable<LastModifiedAttribute> attributesToMethods = method.GetCustomAttributes().OfType<LastModifiedAttribute>();
                if (attributesToMethods.Count() > 0)
                {
                    AddToOutput($"{method.ReturnType} {method.Name}()");
                    foreach (Attribute attribute in attributesToMethods)
                    {
                        WriteAttributeInfo(attribute);
                    }
                }
            }
        }

        private static void WriteAttributeInfo(Attribute attribute)
        {

            LastModifiedAttribute lastModifiedAttribute = attribute as LastModifiedAttribute;

            if (lastModifiedAttribute == null)
            {
                return;
            }

            // check that date is in range
            DateTime modifiedDate = lastModifiedAttribute.DateModified;

            if (modifiedDate < backDateTo)
            {
                return;
            }

            AddToOutput($" modified: {modifiedDate:D}: {lastModifiedAttribute.Changes}");

            if (lastModifiedAttribute.Issues != null)
            {
                AddToOutput($" Outstanding issues: {lastModifiedAttribute.Issues}");
            }
        }
    }

}
