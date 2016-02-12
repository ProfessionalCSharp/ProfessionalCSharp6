using ModelsWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace TemplatesWPF
{
    /// <summary>
    /// Interaction logic for StyledLists.xaml
    /// </summary>
    public partial class StyledLists : Window
    {
        public ObservableCollection<Country> Countries { get; } = new ObservableCollection<Country>();
        public StyledLists()
        {
            InitializeComponent();
            this.DataContext = this;
            var countries = new CountryRepository().GetCountries();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
    }
}
