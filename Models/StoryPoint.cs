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
        public bool PlayerAvailable = false;
        public TimeOfDay TimeOfDay;


        public StoryPoint(string eventText, string eventReason, int dayOfOccurence, TimeOfDay timeOfDay = TimeOfDay.Unspecified, bool pubcli = false)
        {
            EventText = eventText;
            EventReason = eventReason;
            DayOfOccurence = dayOfOccurence;
            TimeOfDay = timeOfDay;
            PlayerAvailable = pubcli;
        }

        public override string ToString()
        {
            return EventText + " " + DayOfOccurence;
        }

        //Character of origin
        //Character Target

    }

    public class CalenderItem
    {
        public string Title;
        public string Description;
        public TimeOfDay TimeOfDay;
        public int DayInInt;

        public CalenderItem(string title, string description, TimeOfDay timeOfDay, int dayInInt)
        {
            Title = title;
            Description = description;
            TimeOfDay = timeOfDay;
            DayInInt = dayInInt;
        }
    }
    public enum TimeOfDay
    {
        Unspecified, Morning, Midday, Evening, Night, AllDay
    }

}
