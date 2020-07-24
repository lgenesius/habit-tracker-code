using System;
using Xunit;

using Npgsql;
using HabitTracker.Database.Postgresql;

namespace HabitTracker.test{
    public class HabitRepoTest{
        private string connString;

        public HabitRepoTest(){
            connString = "Host=localhost;Username=postgres;Password=user;Database=abcapp;Port=5432";
        }

        [Fact]
        public void CreateHabit(){
            NpgsqlConnection _connection = new NpgsqlConnection(connString);
            _connection.Open();

            IHabitRepository repo = new PostgreHabitRepo(_connection, null);
            IUserRepository repo2 = new PostgreUserRepo(_connection, null);
            
            User u = User.NewUser(Guid.NewGuid(), "mayboy");
            repo2.Create(u);

            User u2 = repo2.FindById(u.ID);

            string[] arr = { "Mon", "Tue" };
            _Habit b = _Habit.NewHabit(Guid.NewGuid(), "jogging",arr, u2.ID, DateTime.Now);
            repo.Create(b);

            _Habit b2 = repo.FindByUserId(u.ID, b.ID);
            Assert.NotNull(b2);
            
            Assert.Equal(b.ID, b2.ID);
            Assert.Equal(b.Name, b2.Name);
            Assert.Equal(b.Days_Off.Days, b2.Days_Off.Days);
            Assert.Equal(b.Logs.Log_Count, b2.Logs.Log_Count);
            Assert.Equal(b.Logs.Logs, b2.Logs.Logs);

            b2.doHabit("Mon", u2);
            b2.doHabit("Mon", u2);
            b2.doHabit("Mon", u2);
            b2.doHabit("Mon", u2);
            Assert.Equal("Dominating", u2._Badge[0].Name);

            _connection.Close();
        }
    }
}