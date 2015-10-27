
namespace SyntaxRewriter
{
    class Sample
    {
        // these properties can be converted to auto properties
        private int _x;
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        // this is already a auto property
        public int Y { get; set; }

        // this shouldn't be converted
        private int _z = 3;
        public int Z
        {
            get { return _z; }
        }
    }
}
