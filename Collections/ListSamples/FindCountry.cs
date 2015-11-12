namespace ListSamples
{
    public class FindCountry
    {
        public FindCountry(string country)
        {
            _country = country;
        }
        private string _country;

        public bool FindCountryPredicate(Racer racer) => racer?.Country == _country;
    }
}
