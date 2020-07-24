using System;

namespace HabitTracker.BadgeCollector{
    public class FirstBadge : IBadgeCollector{
        public string[] Collect(){
            string[] arr = { "Dominating", "4+ streak" };
            return arr;
        }
    }

    public class SecondBadge : IBadgeCollector{
        public string[] Collect(){
            string[] arr = { "Workaholic", "Doing some works on days-off" };
            return arr;
        }
    }

    public class ThirdBadge : IBadgeCollector{
        public string[] Collect(){
            string[] arr = { "Epic Comeback", "10 streak after 10 days without logging" };
            return arr;
        }
    }
}