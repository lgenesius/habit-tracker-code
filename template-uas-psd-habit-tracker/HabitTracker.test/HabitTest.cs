using System;
using Xunit;
using System.Collections.Generic;

namespace HabitTracker.test{
    public class HabitTest{
        [Fact]
        public void CheckHabit(){
            User lele = new User(Guid.NewGuid(), "mayboy");

            string[] arr = { "Mon", "Tue" };
            _Habit habit = new _Habit(Guid.NewGuid(), "lari", arr, lele.ID, DateTime.Now);
            Assert.Equal("lari", habit.Name);
            Assert.Equal(lele.ID, habit.userID);

            habit.doHabit("Mon", lele);
            Assert.Equal(1, habit.Logs.Current_Streak);
            Assert.Equal(1, habit.Logs.Log_Count);

            habit.doHabit("Mon", lele);
            habit.doHabit("Mon", lele);
            Assert.Equal(lele.ID, habit.userID);
            
           

            
        }
    }
}