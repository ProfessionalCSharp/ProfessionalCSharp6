using Formula1Demo.Model;
using System;
using System.Collections.Generic;
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

namespace Formula1Demo.Controls
{
    /// <summary>
    /// Interaction logic for GridUC.xaml
    /// </summary>
    public partial class GridUC : UserControl
    {
        private int _currentPage = 0;
        private int _pageSize = 50;

        public GridUC()
        {
            InitializeComponent();
            this.DataContext = Races;

        }

        private IEnumerable<object> GetRaces()
        {
            using (var data = new Formula1Context())
            {
                return (from r in data.Races
                        from rr in r.RaceResults
                        orderby r.Date ascending
                        select new
                        {
                            r.Date.Year,
                            r.Circuit.Country,
                            rr.Position,
                            Racer = rr.Racer.FirstName + " " + rr.Racer.LastName,
                            Car = rr.Team.Name
                        }).Skip(_currentPage * _pageSize).Take(_pageSize).ToList();
            }
        }

        public IEnumerable<object> Races => GetRaces();


        private void OnPrevious(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                this.DataContext = Races;
            }
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            this.DataContext = Races;
        }
    }
}
