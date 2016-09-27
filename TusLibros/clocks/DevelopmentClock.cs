using System;

namespace TusLibros.clocks
{
    class DevelopmentClock : IClock
    {
        private int UpdatedMinutes;
        public DateTime ClockTime { get; set; }

        public DevelopmentClock()
        {
            ClockTime = DateTime.Now;
            UpdatedMinutes = 0;
        }

        public void UpdateSomeMinutes(int minutes)
        {
            UpdatedMinutes = UpdatedMinutes + minutes;
        }

        public DateTime TimeNow()
        {
            ClockTime = DateTime.Now;
            return ClockTime.AddMinutes(UpdatedMinutes);
        }
    }
}
