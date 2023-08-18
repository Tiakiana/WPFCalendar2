using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar
{
    public class Persistence
    {
        public static string MainPath;
        public static string FileName;
        private StoryPointController _storyPointController;
        public Persistence(StoryPointController stp)
        {
            _storyPointController = stp;
            MainPath = AppDomain.CurrentDomain.BaseDirectory;// System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            FileName = "Save1.txt";
        }

        public void SaveDate(int day)
        {
            File.WriteAllText(MainPath +"Date.txt",day.ToString());
        }
     

        public void SaveCalendar()
        {
            string res = "";
            foreach (var item in _storyPointController.StoryPoints)
            {
                res += $"{item.EventText}(,){item.EventReason}(,){item.DayOfOccurence}(n)";
            }
            System.IO.File.WriteAllText(MainPath+FileName,res);
        }

        public List<StoryPoint> LoadCalendar()
        {
            if (!System.IO.File.Exists(MainPath+FileName))
            {
                return new List<StoryPoint>();
            }
            List<StoryPoint> stories = new List<StoryPoint>();
            string text = System.IO.File.ReadAllText(MainPath+FileName);
            string[] lines = text.Split(new string[] { "(n)" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in lines)
            {
                string[] splits = item.Split(new string[] { "(,)" }, StringSplitOptions.RemoveEmptyEntries);
                stories.Add(new StoryPoint(splits[0],splits[1],int.Parse(splits[2])));
            }
            return stories;
        }
        public int LoadDate()
        {
            if (File.Exists(MainPath+"Date.txt"))
            {
                string date = File.ReadAllText(MainPath+"Date.txt");
                return int.Parse(date);
            }
            return 1;
        }

    }
}
