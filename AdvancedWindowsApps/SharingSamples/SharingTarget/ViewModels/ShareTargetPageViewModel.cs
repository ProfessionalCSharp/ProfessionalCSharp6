using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Popups;

namespace SharingTarget.ViewModels
{
    public class ShareTargetPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ShareOperation _shareOperation;
        private readonly ObservableCollection<string> _shareFormats = new ObservableCollection<string>();
        public string SelectedFormat { get; set; }
        public IEnumerable<string> ShareFormats => _shareFormats;
        
        public void Activate(ShareOperation shareOperation)
        {
            string title = null;
            string description = null;
            try
            {
                _shareOperation = shareOperation;

                title = _shareOperation.Data.Properties.Title;
                description = _shareOperation.Data.Properties.Description;
                foreach (var format in _shareOperation.Data.AvailableFormats)
                {
                    _shareFormats.Add(format);
                }

                Title = title;
                Description = description;

            }
            catch (Exception ex)
            {
                _shareOperation.ReportError(ex.Message);
            }
        }

        public void ReportCompleted()
        {
            _shareOperation.ReportCompleted();
        }

        private bool dataRetrieved = false;
        public async void RetrieveData()
        {
            try
            {
                if (dataRetrieved)
                {
                    await new MessageDialog("data already retrieved").ShowAsync();
                }
                _shareOperation.ReportStarted();
                switch (SelectedFormat)
                { 
                    case "Text":
                        Text = await _shareOperation.Data.GetTextAsync();
                        break;
                    case "HTML Format":
                        Html = await _shareOperation.Data.GetHtmlFormatAsync();
                        break;
                    default:
                        break;
                }
                _shareOperation.ReportDataRetrieved();
                dataRetrieved = true;
            }
            catch (Exception ex)
            {

                _shareOperation.ReportError(ex.Message);
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private string _html;
        public string Html
        {
            get { return _html; }
            set
            {
                _html = value;
                OnPropertyChanged();
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }
}
