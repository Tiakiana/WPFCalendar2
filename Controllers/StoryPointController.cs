﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendar
{
    public class StoryPointController
    {
        public List<StoryPoint> StoryPoints = new List<StoryPoint>();

        public List<StoryPoint> CurrentStoryPoints = new List<StoryPoint>();
        public string[] PlayerLocations;
        public List<StoryPoint> FindCurrentStoryPoints(int day)
        {
            List<StoryPoint> storypoints = new List<StoryPoint>();
            foreach (var item in StoryPoints)
            {
                if (item.DayOfOccurence == day)
                {
                    storypoints.Add(item);
                }
            }
            return storypoints;
        }

        public string StringifyStoryPoints()
        {
            string res = "";
            List<StoryPoint> points = StoryPoints.Where(x=> x.PlayerAvailable).ToList();
            for (int i = 0; i < points.Count; i++)
            {
                StoryPoint item = points[i];

                if (item.PlayerAvailable)
                {
                    res += item.EventText + "|";
                    res += item.EventReason + "|";
                    res += (int)item.TimeOfDay + "|";
                    res += item.DayOfOccurence;
                    if (i < points.Count - 1)
                    {
                        res += "¤";
                    }

                }
            }
            return res;
        }
        public string StringifyStoryPoints(int day)
        {
            string res = "";
            List<StoryPoint> points = StoryPoints.Where(x => x.PlayerAvailable && x.DayOfOccurence == day).ToList();
            for (int i = 0; i < points.Count; i++)
            {
                StoryPoint item = points[i];

                if (item.PlayerAvailable)
                {
                    res += item.EventText + "|";
                    res += item.EventReason + "|";
                    res += (int)item.TimeOfDay + "|";
                    res += item.DayOfOccurence;
                    if (i < points.Count - 1)
                    {
                        res += "¤";
                    }

                }
            }
            return res;
        }

        public string ConcatenateStorypoints(List<StoryPoint> storypoints)
        {
            string res = "";
            foreach (var item in storypoints)
            {
                res += (item.PlayerAvailable?"(visible) ":"(Invisible) ")+ item.EventText + "\n\n";
            }
            return res;
        }

        public string ConcatenateReasons(List<StoryPoint> storyPoints)
        {
            string res = "";
            foreach (var item in storyPoints)
            {
                string[] words = item.EventText.Split(' ');
                if (words.Length > 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        res += " " + words[i];
                    }
                }
                res += "...: ";
                res += item.EventReason + "\n\n";
            }
            return res;
        }

        public List<string> ConcatenateReasons(List<StoryPoint> storyPoints, ref List<string> origins)
        {
            origins.Clear();
            List<string> result = new List<string>();
            foreach (var item in storyPoints)
            {
                string originstext = "";
                string res = "";
                string[] words = item.EventText.Split(' ');
                if (words.Length > 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        originstext += words[i] + " ";
                    }
                }
                originstext += "...:";
                origins.Add(originstext);
                res += item.EventReason;
                result.Add(res);
            }
            return result;
        }


    }
}
