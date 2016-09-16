using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TusLibros.clocks
{
    class IntegrationClock : IClock
    {
        public DateTime ClockTime { get; set; }

        public IntegrationClock()
        {
            ClockTime = DateTime.Now;
        }

        public void UpdateSomeMinutes(int minutes)
        {
            throw new NotImplementedException("What are you trying to do, hacker?");
        }

        public DateTime TimeNow()
        {
            return ClockTime;
        }
    }
}
