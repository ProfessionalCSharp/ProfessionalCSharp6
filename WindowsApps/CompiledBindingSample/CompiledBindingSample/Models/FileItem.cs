using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace CompiledBindingSample.Models
{
    public class FileItem
    {
        private IStorageItem _storageItem;
        public FileItem(IStorageItem storageItem)
        {
            _storageItem = storageItem;
        }

        private BasicProperties _basicProperties;

        private async Task<BasicProperties> GetPropertiesAsync()
        {
            if (_basicProperties == null)
            {
                _basicProperties = await _storageItem.GetBasicPropertiesAsync();
            }
            return _basicProperties;
            
        }
        public string FileName => _storageItem.Name;

        //public ulong Size => (await GetPropertiesAsync()).Size;
        public ulong Size
        {
            async get
            {

            }
        }


        private BitmapImage _imageData = new BitmapImage();
        public BitmapImage ImageData => _imageData;

        private async Task LoadImageAsync()
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(_storageItem.Path);
            StorageItemThumbnail tn = await file.GetThumbnailAsync(ThumbnailMode.SingleItem);

            await _imageData.SetSourceAsync(await file.GetThumbnailAsync(ThumbnailMode.SingleItem));
        }

        //public string DisplayName { get; set; }
        //public string Path { get; set; }
        //public int FileSize { get; set; }
        //public DateTimeOffset Date { get; set; }
        //public int ImageWidth { get; private set; }
        //public int ImageHeight { get; private set; }

        //private BitmapImage _imageData;
        //public BitmapImage ImageData { get { return _imageData; } }

        //private async Task FetchImageAsync()
        //{
        //    _imageData = new BitmapImage();
        //    StorageFile f = await StorageFile.GetFileFromPathAsync(Path);
        //    await _imageData.SetSourceAsync(await f.GetThumbnailAsync(ThumbnailMode.SingleItem));
        //}

        //public string Key { get; private set; }

        //internal async static Task<FileItem> fromStorageFile(StorageFile f)
        //{
        //    FileItem item = new FileItem();
        //    item.Path = f.Path;
        //    item.DisplayName = f.DisplayName;
        //    BasicProperties bp = await f.GetBasicPropertiesAsync();
        //    item.FileSize = (int)bp.Size;
        //    item.Date = bp.DateModified;

        //    item.Key = f.FolderRelativeId;
        //    ImageProperties ip = await f.Properties.GetImagePropertiesAsync();
        //    item.ImageWidth = (int)ip.Width;
        //    item.ImageHeight = (int)ip.Height;
        //    var t = item.FetchImageAsync();
        //    return item;
        //}
    }
}
