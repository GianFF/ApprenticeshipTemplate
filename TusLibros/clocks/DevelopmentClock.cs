using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ClockTime = DateTime.Now;
            ClockTime = ClockTime.AddMinutes(minutes);
        }

        public DateTime TimeNow()
        {
            return ClockTime;
        }
    }
}
