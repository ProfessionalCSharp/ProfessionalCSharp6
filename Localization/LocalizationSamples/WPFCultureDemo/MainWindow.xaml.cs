using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCultureDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetupCultures();
        }

        private void SetupCultures()
        {
            var cultureDataDict = CultureInfo.GetCultures(CultureTypes.AllCultures)
              .OrderBy(c => c.Name)
              .Select(c => new CultureData
              {
                  CultureInfo = c,
                  SubCultures = new List<CultureData>()
              })
              .ToDictionary(c => c.CultureInfo.Name);

            var rootCultures = new List<CultureData>();
            foreach (var cd in cultureDataDict.Values)
            {
                if (cd.CultureInfo.Parent.LCID == 127)
                {
                    rootCultures.Add(cd);
                }
                else
                {
                    CultureData parentCultureData;
                    if (cultureDataDict.TryGetValue(cd.CultureInfo.Parent.Name,
                      out parentCultureData))
                    {
                        parentCultureData.SubCultures.Add(cd);
                    }
                    else
                    {
                        throw new ParentCultureException(
                          "unexpected error - parent culture not found");
                    }
                }
            }
            this.DataContext = rootCultures.OrderBy(cd => cd.CultureInfo.EnglishName);

        }

        private void treeCultures_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            itemGrid.DataContext =  e.NewValue as CultureData;
        }
    }
}
