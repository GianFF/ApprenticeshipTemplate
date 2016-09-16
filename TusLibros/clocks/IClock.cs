using System;

namespace TusLibros.clocks
{
    public interface IClock
    {
        DateTime ClockTime { get; set; }

        void UpdateSomeMinutes(int minutes);

        DateTime TimeNow();
    }
}