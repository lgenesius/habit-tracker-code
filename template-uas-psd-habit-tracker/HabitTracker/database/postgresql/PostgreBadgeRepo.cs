using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace HabitTracker.Database.Postgresql{
    public class PostgreBadgeRepo : IBadgeRepository{
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public PostgreBadgeRepo(NpgsqlConnection connection, NpgsqlTransaction transaction){
            _connection = connection;
            _transaction = transaction;
        }

        public void Create(_Badge badge){
            string query = "INSERT INTO \"badge\" (id,name,description,user_id) VALUES(@id,@name,@description,@user_id)";

            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("id",badge.ID);
                cmd.Parameters.AddWithValue("name",badge.Name);
                cmd.Parameters.AddWithValue("description",badge.Description);
                cmd.Parameters.AddWithValue("user_id",badge.User_Id);
                cmd.ExecuteNonQuery();
            }
        }

        public _Badge FindByUserId(Guid user_id){
            string query = "select id,name,description, created_at from \"badge\" where user_id = @user_id";
            Guid id;
            string name = "";
            string description = "";
            DateTime createdDate;

            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("user_id", user_id);
                using(NpgsqlDataReader reader = cmd.ExecuteReader()){
                    if(reader.Read()){
                        id = reader.GetGuid(0);
                        name = reader["name"] as string;
                        description = reader["description"] as string;
                        createdDate = reader["created_at"] as DateTime? ?? throw new Exception("DateTime error");
                    }
                    else{
                        return null;
                    }
                }
            }

            _Badge bdg = new _Badge(id, name, description, user_id, createdDate);
            return bdg;
        }

        public List<_Badge> GetAllBadge(Guid user_id){
            string query = "SELECT id,name,description,created_at FROM \"badge\" WHERE user_id = @userID";
            List<_Badge> get = new List<_Badge>();

            using(var cmd = new NpgsqlCommand(query, _connection,_transaction)){
                cmd.Parameters.AddWithValue("userID", user_id);
                using(NpgsqlDataReader reader = cmd.ExecuteReader()){
                    while(reader.Read()){
                        get.Add(new _Badge(reader["id"] as Guid? ?? throw new Exception("Guid error"), reader["name"] as string, reader["description"] as string, user_id, reader["created_at"] as DateTime? ?? throw new Exception("DateTime error")));
                    }
                }

                return get;
            }
        }
    }
}