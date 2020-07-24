using System;
using System.Collections.Generic;
using HabitTracker.BadgeCollector;

namespace HabitTracker
{
    public class _Habit
    {
        private Guid id;
        private string name;
        private DaysOff days_off;
        private Log logs;
        private Guid user_id;
        private DateTime created_at;

        public Guid ID{
            get{
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Guid userID
        {
            get
            {
                return user_id;
            }
        }

        public DaysOff Days_Off{
            get{
                return days_off;
            }
        }

        public Log Logs{
            get{
                return logs;
            }
        }

        public DateTime Created_At{
            get{
                return created_at;
            }
        }

        public _Habit(Guid id, string name, string[] days_off, Guid user_id, DateTime created_at)
        {
            if (checkChar(name) == false)
            {
                throw new Exception("Name does not match the requirement");
            }
            this.id = id;
            this.name = name;
            this.days_off = new DaysOff(days_off);
            this.user_id = user_id;
            this.created_at = created_at;

            this.logs = new Log();
        }

        public void doHabit(string day, User us)
        {
            IBadgeCollector firstCollect = new FirstBadge();
            IBadgeCollector secondCollect = new SecondBadge();
            IBadgeCollector thirdCollect = new ThirdBadge();

            BadgeFactory bdgFactory = new BadgeFactory();

            if (days_off.checkRightDays(day) == false)
            {
                throw new Exception("The day format is wrong!");
            }

            if(us.ID != user_id){
                throw new Exception("Wrong user!");
            }

            if (days_off.checkMatchDays(day))
            {
                logs.RecordLog();
                logs.ReSetSkipCount();
                if (CheckFirstBadgeDuplicate(us._Badge))
                {
                    if (logs.Current_Streak == 4)
                    {
                        bdgFactory.CreateFirstBadge(firstCollect, us._Badge, us.ID);
                    }
                }
                else if (CheckSecondBadgeDuplicate(us._Badge))
                {
                    if(us.Total_Log_User >= 10){
                        bdgFactory.CreateSecondBadge(secondCollect, us._Badge, us.ID);
                    }
                }
                else if (CheckThirdBadgeDuplicate(us._Badge))
                {
                    if(logs.Get_Skip_Streak == true && logs.Current_Streak == 10){
                        bdgFactory.CreateThirdBadge(thirdCollect, us._Badge, us.ID);
                    }
                }
            }
            else
            {
                logs.ResetStreak();
                logs.AddSkipCount();
            }

            us.AddTotalLogUser();
        }

        private bool CheckFirstBadgeDuplicate(List<_Badge> bdg){
            for(int i=0; i < bdg.Count; i++){
                if(bdg[i].Name == "Dominating"){
                    return false;
                }
            }

            return true;
        }

        private bool CheckSecondBadgeDuplicate(List<_Badge> bdg){
            for(int i=0; i < bdg.Count; i++){
                if(bdg[i].Name == "Workaholic"){
                    return false;
                }
            }

            return true;
        }

        private bool CheckThirdBadgeDuplicate(List<_Badge> bdg){
            for(int i=0; i < bdg.Count; i++){
                if(bdg[i].Name == "Epic Comeback"){
                    return false;
                }
            }

            return true;
        }

        public bool checkChar(string name)
        {
            int count = 0;
            bool check = true;
            foreach (char c in name)
            {
                count++;
            }

            if (count < 2 || count > 100)
            {
                check = false;
            }

            return check;
        }

        public static _Habit NewHabit(Guid id, string name, string[] days_off, Guid user_id, DateTime created_at){
            return new _Habit(id, name, days_off, user_id, created_at);
        }


        public override bool Equals(object obj)
        {
            var habit = obj as _Habit;
            if (habit == null) return false;

            return this.id == habit.id;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}