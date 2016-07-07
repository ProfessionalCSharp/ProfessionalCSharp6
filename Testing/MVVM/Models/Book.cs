using Framework;

namespace Models
{
    public class Book : BindableBase
    {
        public int BookId { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _publisher;
        public string Publisher
        {
            get { return _publisher; }
            set { SetProperty(ref _publisher, value); }
        }

        public override string ToString() => Title;
    }
}
