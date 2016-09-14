using System;
using System.Windows.Forms.VisualStyles;

namespace TusLibros
{
    public class Clock
    {
        public DateTime ClockTime { get; set; }

        public Clock()
        {
            ClockTime = DateTime.Now; // TODO ver como mantener la fecha siempre actualizada
        }

        public void UpdateSomeMinutes(int minutes)
        {
            ClockTime = DateTime.Now; // TODO ver como mantener la fecha siempre actualizada
            ClockTime = ClockTime.AddMinutes(minutes);
        }

        public DateTime TimeNow()
        {
            ClockTime = DateTime.Now; // TODO ver como mantener la fecha siempre actualizada
            return ClockTime;
        }
    }
}