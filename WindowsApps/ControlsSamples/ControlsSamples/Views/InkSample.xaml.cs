using ControlsSamples.Utilities;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ControlsSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InkSample : Page
    {
        public InkSample()
        {
            this.InitializeComponent();
            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Touch | CoreInputDeviceTypes.Pen;
            ColorSelection = new ColorSelection(inkCanvas);
        }

        public ColorSelection ColorSelection { get; }

        private const string FileTypeExtension = ".strokes";
        public async void OnSave()
        {
            var picker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                DefaultFileExtension = FileTypeExtension,
                SuggestedFileName = "sample"
            };
            picker.FileTypeChoices.Add("Stroke File", new List<string>() { FileTypeExtension });
            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                using (StorageStreamTransaction tx = await file.OpenTransactedWriteAsync())
                {
                    await inkCanvas.InkPresenter.StrokeContainer.SaveAsync(tx.Stream);
                    await tx.CommitAsync();
                }
            }
        }

        public async void OnLoad()
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(FileTypeExtension);

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                using (var stream = await file.OpenReadAsync())
                {
                    await inkCanvas.InkPresenter.StrokeContainer.LoadAsync(stream);
                }
            }
        }

        public void OnClear()
        {
            inkCanvas.InkPresenter.StrokeContainer.Clear();
        }
    }
}
