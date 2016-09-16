using System;

namespace TusLibros.lib
{
    public class Clock
    {
        public DateTime ClockTime { get; set; }

        public Clock()
        {
            ClockTime = DateTime.Now;
        }

        public void UpdateSomeMinutes(int minutes)
        {
            ClockTime = DateTime.Now;
            ClockTime = ClockTime.AddMinutes(minutes);
        }

        public DateTime TimeNow()
        {
            return ClockTime;
        }
    }
}