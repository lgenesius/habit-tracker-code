using System;
using Xunit;

namespace HabitTracker.test
{
    public class UserTest
    {
        [Fact]
        public void CheckUser()
        {
            User lele = User.NewUser(Guid.NewGuid(), "mayboy");
            Assert.Equal("mayboy", lele.Name);
        }
    }
}
