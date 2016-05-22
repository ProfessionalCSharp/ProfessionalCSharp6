using CompiledBindingSample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CompiledBindingSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LifetimeSample : Page
    {
        public LifetimeSample()
        {
            this.InitializeComponent();
        }


        public Book Book { get; } = new Book { Title = "Professional C# 6", Publisher = "Wrox Press" };

        public void OnChangeBook()
        {
            Book.Title = "Professional C# 6 and .NET Core 1.0";
        }

        private void OnUpdateBinding()
        {
            Bindings.Update();
          
        }

        private void OnStopTracking()
        {
            Bindings.StopTracking();
        }
    }
}
