using System;
using Xunit;

namespace HabitTracker.test{
    public class DaysOffTest{
        [Fact]
        public void CheckTheDay(){
            string[] arr = { "Mon", "Tue" };
            DaysOff str = new DaysOff(arr);
            Assert.Equal("Mon", str.Days[0]);
            Assert.Equal("Tue", str.Days[1]);
        }
    }
}