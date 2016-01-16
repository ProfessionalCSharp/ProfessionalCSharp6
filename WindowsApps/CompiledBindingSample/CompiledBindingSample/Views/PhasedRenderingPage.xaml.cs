using CompiledBindingSample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class PhasedRenderingPage : Page
    {
        private ObservableCollection<SomeData> _someData;

        public PhasedRenderingPage()
        {

            _someData = new ObservableCollection<SomeData>(SomeDataFactory.GetSampleData(100000));
            this.InitializeComponent();
        }

        public IEnumerable<SomeData> SomeData => _someData;

        public IEnumerable<FileItem> FileItems => _fileItems;
        ObservableCollection<FileItem> _fileItems = new ObservableCollection<FileItem>();
 

        private async void OnOpenFiles(object sender, RoutedEventArgs e)
        {

            var picker = new FolderPicker();
     

            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
               
                IReadOnlyList<IStorageItem> items = await folder.GetItemsAsync();
                var fileItems = items.Select(item => new FileItem(item));
                foreach (var item in fileItems)
                {
                    _fileItems.Add(item);

                }
            }



        }
    }
}
