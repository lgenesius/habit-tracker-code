using System;
using Xunit;

using Npgsql;
using HabitTracker.Database.Postgresql;

namespace HabitTracker.test{
    public class UserRepoTest{
        private string connString;

        public UserRepoTest(){
            connString = "Host=localhost;Username=postgres;Password=user;Database=abcapp;Port=5432";
        }

        [Fact]
        public void CreateUser(){
            NpgsqlConnection _connection = new NpgsqlConnection(connString);
            _connection.Open();

            IUserRepository repo = new PostgreUserRepo(_connection, null);

            User u = User.NewUser(Guid.NewGuid(), "mayboy");
            repo.Create(u);

            User u2 = repo.FindById(u.ID);
            Assert.NotNull(u2);

            Assert.Equal(u.ID, u2.ID);
            Assert.Equal(u.Name, u2.Name);

            _connection.Close();
        }
    }
}