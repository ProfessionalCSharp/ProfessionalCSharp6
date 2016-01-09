using Formula1Demo.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Formula1Demo.Controls
{
    /// <summary>
    /// Interaction logic for TreeUC.xaml
    /// </summary>
    public partial class TreeUC : UserControl
    {
        public TreeUC()
        {
            InitializeComponent();
            this.DataContext = Years;
        }

        private List<Championship> _years;

        private List<Championship> GetYears()
        {
            using (var data = new Formula1Context())
            {
                return data.Races.Select(r => new Championship
                {
                    Year = r.Date.Year
                }).Distinct().OrderBy(c => c.Year).ToList();
            }
        }

        public IEnumerable<Championship> Years => _years ?? (_years = GetYears());

    }
}
