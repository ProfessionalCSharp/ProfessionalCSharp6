using System;

namespace AppSupport
{
    public class DateService
    {
        public string GetLongDateInfoString() => $"Today's date is {DateTime.Today:D}";

        public string GetShortDateInfoString() => $"Today's date is {DateTime.Today:d}";
    }
}
