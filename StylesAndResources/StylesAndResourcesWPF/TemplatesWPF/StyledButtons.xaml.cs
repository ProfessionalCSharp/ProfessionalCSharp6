using ModelsWPF;
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
using System.Windows.Shapes;

namespace TemplatesWPF
{
    /// <summary>
    /// Interaction logic for StyledButtons.xaml
    /// </summary>
    public partial class StyledButtons : Window
    {
        public StyledButtons()
        {
            InitializeComponent();

            this.countryButton.Content = new Country
            {
                Name = "Austria",
                ImagePath = "/Images/Austria.bmp"
            };
        }
    }
}
