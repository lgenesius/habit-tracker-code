using System;
using System.Collections.Generic;

namespace HabitTracker{
    public class DaysOff{
        private string[] days;

        public string[] Days{
            get{
                return days;
            }
        }

        public DaysOff(string[] days){
            if(days.Length<1){
                throw new Exception("Must input days off!");
            }

            if(DayCount(days) == false){
                throw new Exception("Days must not same or more than 7!");
            }

            if(DayCheck(days) == false){
                throw new Exception("Name of days does not match the requirement!");
            }

            if(checkDuplicate(days) == false){
                throw new Exception("Does not accept duplicates");
            }

            this.days = days;
        }

        private bool checkDuplicate(string[] days){
            bool check = true;
            int mon = 0, tue = 0, wed = 0, thu = 0, fri = 0, sat = 0, sun = 0;
            for(int i=0; i<= days.Length-1; i++){
                if(days[i] == "Mon"){
                    mon++;
                }
                else if(days[i] == "Tue"){
                    tue++;
                }
                else if(days[i] == "Wed"){
                    wed++;
                }
                else if(days[i] == "Thu"){
                    thu++;
                }
                else if(days[i] == "Fri"){
                    fri++;
                }
                else if(days[i] == "Sat"){
                    sat++;
                }
                else{
                    sun++;
                }
            }

            if(mon>1 || tue>1 || wed>1 || thu>1 || fri>1 || sat>1 || sun>1){
                check = false;
            }

            return check;
        }

        public bool checkMatchDays(string day){
            bool check = false;
            for(int i = 0; i<= days.Length-1; i++){
                if(days[i] == day){
                    check = true;
                    break;
                }
            }
            return check;
        }

        public bool checkRightDays(string day){
            bool check = false;
            if(day == "Mon" || day == "Tue" || day == "Wed" || day == "Thu" || day == "Fri" || day == "Sat" || day == "Sun"){
                check = true;
            }
            return check;
        }

        private bool DayCheck(string[] days){
            bool check = true;
            for(int i=0; i<= days.Length-1; i++){
                if(days[i] != "Mon" && days[i] != "Tue" && days[i] != "Wed" && days[i] != "Thu" && days[i] != "Fri" && days[i] != "Sat" && days[i] != "Sun" ){
                    check = false;
                    break;
                }
            }
            return check;
        }

        private bool DayCount(string[] days){
            bool check = true;
            if(days.Length >= 7){
                check = false;
            }

            return check;
        }



        public override bool Equals(object obj){
            var days = obj as DaysOff;
            if(days == null) return false;

            return this.days == days.days;
        }

        public override int GetHashCode(){
            return base.GetHashCode();
        } 
    }
}