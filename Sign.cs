using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar
{
    internal class Sign
    {
        public string EventText, EventReason;
        public int DayOfOccurence;
        public int DayOfOriginOfMajor;
        public Sign(string eventText, string eventReason, int dayOfOccurence)
        {
            EventText = eventText;
            EventReason = eventReason;
            DayOfOccurence = dayOfOccurence;
        }

        public override string ToString()
        {
            return EventText + " " + DayOfOccurence;
        }


    }
}
