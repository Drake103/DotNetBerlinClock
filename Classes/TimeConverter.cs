using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            int hours, minutes, seconds;
            if (!tryParseTime(aTime, out hours, out minutes, out seconds))
            {
                throw new ArgumentException(aTime);
            }

            return new BerlinUhr(hours, minutes, seconds).ToString();
        }
        
        private static bool tryParseTime(string aTime, out int aHours, out int aMinutes, out int aSeconds)
        {
            string[] parts = aTime.Split(':');

            aHours = 0;
            aMinutes = 0;
            aSeconds = 0;

            return parts.Count() == 3

                   && int.TryParse(parts[0], out aHours)
                   && aHours <= 24 && aHours >= 0

                   && int.TryParse(parts[1], out aMinutes)
                   && aMinutes <= 59 && aMinutes >= 0

                   && int.TryParse(parts[2], out aSeconds)
                   && aSeconds <= 59 && aSeconds >= 0;
        }
    }
}
