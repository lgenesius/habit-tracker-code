using System;
using Xunit;

namespace HabitTracker.test{
    public class BadgeTest{
        [Fact]
        public void checkBadge(){
            _Badge bg = new _Badge(Guid.NewGuid(), "Dominating", "4+ streak", Guid.NewGuid(), DateTime.Now);
            Assert.Equal("Dominating", bg.Name);
        }
    }
}