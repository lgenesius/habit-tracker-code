using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Npgsql;
using NpgsqlTypes;

namespace HabitTracker.Database.Postgresql
{
    public class PostgreConnectRepo : ConnectRepo
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;
        private IHabitRepository _habitRepo;
        private IUserRepository _userRepo;
        private IBadgeRepository _badgeRepo;

        protected bool dispose2 = false;

        public PostgreConnectRepo()
        {
            string connString = "Host=localhost;Username=postgres;Password=user;Database=abcapp;Port=5432";
            _connection = new NpgsqlConnection(connString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IUserRepository UserRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new PostgreUserRepo(_connection, _transaction);
                }
                return _userRepo;
            }
        }

        public IBadgeRepository BadgeRepo{
            get{
                if(_badgeRepo == null){
                    _badgeRepo = new PostgreBadgeRepo(_connection, _transaction);
                }
                return _badgeRepo;
            }
        }

        public IHabitRepository HabitRepo
        {
            get
            {
                if (_habitRepo == null)
                {
                    _habitRepo = new PostgreHabitRepo(_connection, _transaction);
                }
                return _habitRepo;
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!this.dispose2)
            {
                if (dispose)
                {
                    _connection.Close();
                }

                dispose2 = true;
            }
        }
    }
}