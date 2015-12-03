using Microsoft.Scripting.Hosting;
using System;
using System.Windows;

namespace DLRHostSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCalculateDiscount(object sender, RoutedEventArgs e)
        {
            string scriptToUse;
            if (CostRadioButton.IsChecked.Value)
            {
                scriptToUse = "Scripts/AmountDisc.py";
            }
            else
            {
                scriptToUse = "Scripts/CountDisc.py";
            }
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            ScriptEngine rbEng = scriptRuntime.GetEngine("python");
            ScriptSource source = rbEng.CreateScriptSourceFromFile(scriptToUse);
            ScriptScope scope = rbEng.CreateScope();
            scope.SetVariable("prodCount", Convert.ToInt32(totalItems.Text));
            scope.SetVariable("amt", Convert.ToDecimal(totalAmt.Text));
            source.Execute(scope);
            textDiscAmount.Text = scope.GetVariable("retAmt").ToString();
        }

        private void OnCalculateTax(object sender, RoutedEventArgs e)
        {
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            dynamic calcRate = scriptRuntime.UseFile("Scripts/CalcTax.py");
            decimal discountedAmount;
            if (!decimal.TryParse(textDiscAmount.Text, out discountedAmount))
            {
                discountedAmount = Convert.ToDecimal(totalAmt.Text);
            }
            textTaxAmount.Text = calcRate.CalcTax(discountedAmount).ToString();
        }
    }
}
