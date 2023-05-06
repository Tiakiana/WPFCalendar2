using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar
{
  public class StoryPoint
    {
        public string EventText, EventReason;
        public int DayOfOccurence;

        public StoryPoint(string eventText, string eventReason, int dayOfOccurence)
        {
            EventText = eventText;
            EventReason = eventReason;
            DayOfOccurence = dayOfOccurence;
        }
        //Character of origin
        //Character Target

    }
}
