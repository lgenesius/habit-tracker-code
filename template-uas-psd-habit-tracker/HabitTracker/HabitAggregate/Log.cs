using System;
using System.Collections.Generic;

namespace HabitTracker{
    public class Log{
        private DateTime[] logs;
        private int log_count;
        private int longest_streak;
        private int current_streak;
        private int skip_count;
        private bool get_skip_streak = false;

        public DateTime[] Logs{
            get{
                return logs;
            }
        }

        public int Current_Streak{
            get{
                return current_streak;
            }
        }

        public int Longest_Streak{
            get{
                return longest_streak;
            }
        }

        public bool Get_Skip_Streak{
            get{
                return get_skip_streak;
            }
        }

        public int Log_Count{
            get{
                return log_count;
            }
        }

        public Log(){
            logs = new DateTime[100];
            log_count = 0;
            longest_streak = 0;
            current_streak = 0;
            skip_count = 0;
            get_skip_streak = false;
        }

        public void SetTheLog(DateTime[] logs, int log_count, int longest_streak, int current_streak){
            this.logs = logs;
            this.log_count = log_count;
            this.longest_streak = longest_streak;
            this.current_streak = current_streak;
        }

        public void ReSetSkipCount(){
            skip_count = 0;
        }

        public void AddSkipCount(){ 
            skip_count++;
            if(skip_count >= 10){
                get_skip_streak = true;
            }
        }

        public void RecordLog(){
            logs[log_count] = DateTime.Now;
            log_count++;
            current_streak++;
            if(current_streak >= longest_streak){
                longest_streak = current_streak;
            }
        }

        public void ResetStreak(){
            current_streak = 0;
        }

        public void SetLogs(DateTime[] logs){
            this.logs = logs;
            
        }
    }
}