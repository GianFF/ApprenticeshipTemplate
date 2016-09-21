using System;

namespace TusLibros.clocks
{
    class DevelopmentClock : IClock
    {
        public DateTime ClockTime { get; set; }

        public DevelopmentClock()
        {
            ClockTime = DateTime.Now;
        }

        public void UpdateSomeMinutes(int minutes)
        {
            ClockTime = DateTime.Now.AddMinutes(minutes);
        }

        public DateTime TimeNow()
        {
            return ClockTime;
        }
    }
}
