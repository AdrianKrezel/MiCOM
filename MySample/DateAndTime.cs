using System;

namespace MySample
{
    /// <summary>
    /// Class that converts seconds to date and time format
    /// </summary>
    /// <param name="sec">Seconds counted since 01/01/1994</param>
    internal class DateAndTime
    {
        public Double Year { get { return year; } }
        public Double Month { get { return month; } }
        public Double Day { get { return day; } }
        public Double Hour { get { return hour; } }
        public Double Minute { get { return minute; } }
        public Double Second { get { return second; } }
        public Double Millisecond { get { return millisecond; } }

        private Double year, month, day, hour, minute, second, millisecond;
        private ushort sec;

        public DateAndTime(ushort sec) 
        {
            this.sec = sec;
            TimeSpan time = TimeSpan.FromSeconds(this.sec);
            DateTime dateTime = DateTime.Today.Add(time);

            this.year = dateTime.Year;
            this.month = dateTime.Month;
            this.day = dateTime.Day;
            this.hour = dateTime.Hour;
            this.minute = dateTime.Minute;
            this.second = dateTime.Second;
            this.millisecond = dateTime.Millisecond;
        }
    }
}
