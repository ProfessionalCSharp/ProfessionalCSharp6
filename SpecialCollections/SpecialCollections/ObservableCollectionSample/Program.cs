using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static System.Console;

namespace ObservableCollectionSample
{
    class Program
    {
        static void Main()
        {
            var data = new ObservableCollection<string>();
            
            data.CollectionChanged += Data_CollectionChanged;
            data.Add("One");
            data.Add("Two");
            data.Insert(1, "Three");
            data.Remove("One");

            data.CollectionChanged -= Data_CollectionChanged;

            ReadLine();
        }

        public static void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            WriteLine($"action: {e.Action.ToString()}");

            if (e.OldItems != null)
            {
                WriteLine($"starting index for old item(s): {e.OldStartingIndex}");
                WriteLine("old item(s):");
                foreach (var item in e.OldItems)
                {
                    WriteLine(item);
                }
            }
            if (e.NewItems != null)
            {
                WriteLine($"starting index for new item(s): {e.NewStartingIndex}");
                WriteLine("new item(s): ");
                foreach (var item in e.NewItems)
                {
                    WriteLine(item);
                }
            }
            WriteLine();
        }
    }
}
