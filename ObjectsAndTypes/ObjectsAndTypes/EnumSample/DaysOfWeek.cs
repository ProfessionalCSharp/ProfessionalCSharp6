using System;

namespace EnumSample
{
    [Flags]
    public enum DaysOfWeek
    {
        Monday = 0x1,
        Tuesday = 0x2,
        Wednesday = 0x4,
        Thursday = 0x8,
        Friday = 0x10,
        Saturday = 0x20,
        Sunday = 0x40,
        Weekend = 0x60,
        Workday = 0x1f,
        AllWeek = 0x7f
    }
}