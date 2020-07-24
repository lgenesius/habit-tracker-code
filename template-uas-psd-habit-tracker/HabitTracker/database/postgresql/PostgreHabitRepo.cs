using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

using System.Text.Json;
using System.Text.Json.Serialization;


namespace HabitTracker.Database.Postgresql{
    public class PostgreHabitRepo : IHabitRepository{
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public PostgreHabitRepo(NpgsqlConnection connection, NpgsqlTransaction transaction){
            _connection = connection;
            _transaction = transaction;
        }

        public void Create(_Habit habit){
            string query = "INSERT INTO \"habit\" (id,name,user_id) VALUES (@id,@name,@user_id)";
            using (var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("id", habit.ID);
                cmd.Parameters.AddWithValue("name", habit.Name);
                cmd.Parameters.AddWithValue("user_id", habit.userID);
                cmd.ExecuteNonQuery();
            }

            string query2 = "INSERT INTO \"daysoff\" (id,days_off,habit_id) VALUES (@id,@days_off,@habit_id)";
            using (var cmd2 = new NpgsqlCommand(query2, _connection, _transaction)){
                cmd2.Parameters.AddWithValue("id",Guid.NewGuid());
                cmd2.Parameters.AddWithValue("days_off",habit.Days_Off.Days);
                cmd2.Parameters.AddWithValue("habit_id",habit.ID);
                cmd2.ExecuteNonQuery();
            }

            string query3 = "INSERT INTO \"log\" (id,logs,log_count,longest_streak,current_streak,habit_id) VALUES(@id,@logs,@log_count,@longest_streak,@current_streak,@habit_id)";
            using (var cmd3 = new NpgsqlCommand(query3, _connection, _transaction)){
                cmd3.Parameters.AddWithValue("id",Guid.NewGuid());
                cmd3.Parameters.AddWithValue("logs",habit.Logs.Logs);
                cmd3.Parameters.AddWithValue("log_count",habit.Logs.Log_Count);
                cmd3.Parameters.AddWithValue("longest_streak", habit.Logs.Longest_Streak);
                cmd3.Parameters.AddWithValue("current_streak", habit.Logs.Current_Streak);
                cmd3.Parameters.AddWithValue("habit_id",habit.ID);
                cmd3.ExecuteNonQuery();
            }
        }

        public _Habit FindByUserId(Guid userId, Guid habit_id){
            string query = "select name, created_at from \"habit\" where user_id = @user_id AND id = @habit_id";
            
            string name = "";
            string[] days_off;
            DateTime createdDate;
            DateTime[] logs;
            int log_count = 0;
            int longest_streak = 0;
            int current_streak = 0;

            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("user_id", userId);
                cmd.Parameters.AddWithValue("habit_id", habit_id);
                using(NpgsqlDataReader reader = cmd.ExecuteReader()){
                    if(reader.Read()){
                        name = reader["name"] as string;
                        createdDate = reader["created_at"] as DateTime? ?? throw new Exception("DateTime error");
                    }
                    else{
                        return null;
                    }
                }   
            }

            string query2 = "select days_off from \"daysoff\" where habit_id = @habit_id";
            using(var cmd2 = new NpgsqlCommand(query2, _connection, _transaction)){
                cmd2.Parameters.AddWithValue("habit_id", habit_id);
                using(NpgsqlDataReader reader = cmd2.ExecuteReader()){
                    if(reader.Read()){
                        days_off = reader["days_off"] as string[];
                    }
                    else{
                        return null;
                    }
                }
            }

            string query3 = "select logs, log_count, longest_streak, current_streak from \"log\" where habit_id = @habit_id";
            using(var cmd3 = new NpgsqlCommand(query3, _connection, _transaction)){
                cmd3.Parameters.AddWithValue("habit_id", habit_id);
                using(NpgsqlDataReader reader = cmd3.ExecuteReader()){
                    if(reader.Read()){
                        logs = reader["logs"] as DateTime[];
                        log_count = reader["log_count"] as int? ?? throw new Exception("Empty log_count");
                        longest_streak = reader["longest_streak"] as int? ?? throw new Exception("Empty longest_streak");
                        current_streak = reader["current_streak"] as int? ?? throw new Exception("Empty current_streak");
                    }   
                    else{
                        return null;
                    }
                }
            }

            _Habit hbt = new _Habit(habit_id, name, days_off, userId, createdDate);
            hbt.Logs.SetTheLog(logs, log_count, longest_streak, current_streak);
            return hbt;
        }

        public void UpdateName(Guid id, string _name, Guid userId){
            string query = "UPDATE habit SET name = @name WHERE id = @id AND user_id = @userID";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("userID", userId);
                cmd.Parameters.AddWithValue("name", _name);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateNameAndDays(Guid id, string _name, string[] days_off, Guid userId){
            string query = "UPDATE habit SET name = @name WHERE id = @id AND user_id = @userID";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("userID", userId);
                cmd.Parameters.AddWithValue("name", _name);
                cmd.ExecuteNonQuery();
            }

            string query2 = "UPDATE daysoff SET days_off = @days_off WHERE habit_id = @id";
            using(var cmd2 = new NpgsqlCommand(query2, _connection, _transaction)){
                cmd2.Parameters.AddWithValue("id", id);
                cmd2.Parameters.AddWithValue("days_off", days_off);
                cmd2.ExecuteNonQuery();
            }
        }

        public void UpdateLog(Guid id, _Habit habit){
            string query = "UPDATE log SET logs = @logs, log_count = @log_count, longest_streak = @longest_streak, current_streak = @current_streak WHERE habit_id = @id";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("id",habit.ID);
                cmd.Parameters.AddWithValue("logs", habit.Logs.Logs);
                cmd.Parameters.AddWithValue("log_count", habit.Logs.Log_Count);
                cmd.Parameters.AddWithValue("longest_streak", habit.Logs.Longest_Streak);
                cmd.Parameters.AddWithValue("current_streak", habit.Logs.Current_Streak);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteHabit(Guid id, Guid userId){
            string query = "DELETE FROM log WHERE habit_id = @id";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }

            string query2 = "DELETE FROM daysoff WHERE habit_id = @id";
            using(var cmd2 = new NpgsqlCommand(query2, _connection, _transaction)){
                cmd2.Parameters.AddWithValue("id", id);
                cmd2.ExecuteNonQuery();
            }

            string query3 = "DELETE FROM habit WHERE id = @id AND user_id = @userID";
            using(var cmd3 = new NpgsqlCommand(query3, _connection, _transaction)){
                cmd3.Parameters.AddWithValue("id", id);
                cmd3.Parameters.AddWithValue("userID", userId);
                cmd3.ExecuteNonQuery();
            }
        }

        public List<_Habit> GetAllHabit(Guid user_id){
            string query = "select id,name,created_at from \"habit\" where user_id = @user_id";
            _Habit temporary;
            
            int i =0;
            List<_Habit> hab = new List<_Habit>();

            List<Guid> id = new List<Guid>();
            List<string> name = new List<string>();
            List<DateTime> createdDate = new List<DateTime>();
            List<string[]> days_off = new List<string[]>();

            List<DateTime[]> logs = new List<DateTime[]>();
            List<int> log_count = new List<int>();
            List<int> longest_streak = new List<int>();
            List<int> current_streak = new List<int>();

            using(var cmd = new NpgsqlCommand(query, _connection, _transaction)){
                cmd.Parameters.AddWithValue("user_id", user_id);
                using(NpgsqlDataReader reader = cmd.ExecuteReader()){
                    while(reader.Read()){
                        id.Add(reader["id"] as Guid? ?? throw new Exception("ID Error"));
                        name.Add(reader["name"] as string);
                        createdDate.Add(reader["created_at"] as DateTime? ?? throw new Exception("DateTime error"));
                    }
                }
            }

            string query2 = "select days_off from \"daysoff\" where habit_id = @habit_id";
            using(var cmd2 = new NpgsqlCommand(query2, _connection, _transaction)){
                while(i < id.Count){
                     cmd2.Parameters.AddWithValue("habit_id", id[i]);
                     using(NpgsqlDataReader reader = cmd2.ExecuteReader()){
                         while(reader.Read()){
                             days_off.Add(reader["days_off"] as string[]);
                         }
                     }
                    i++;
                }
            }

            i = 0;

            string query3 = "select logs,log_count,longest_streak,current_streak from \"log\" where habit_id = @habit_id";
            using(var cmd3 = new NpgsqlCommand(query3, _connection, _transaction)){
                while(i < id.Count){
                    cmd3.Parameters.AddWithValue("habit_id", id[i]);
                    using(NpgsqlDataReader reader = cmd3.ExecuteReader()){
                        while(reader.Read()){
                            logs.Add(reader["logs"] as DateTime[]);
                            log_count.Add(reader["log_count"] as int? ?? throw new Exception("Empty log_count"));
                            longest_streak.Add(reader["longest_streak"] as int? ?? throw new Exception("Empty longest_streak"));
                            current_streak.Add(reader["current_streak"] as int? ?? throw new Exception("Empty current_streak"));
                        }
                    }
                    i++;
                }
            }

            i = 0;

            for(int j = 0; j < id.Count; j++){
                temporary = new _Habit(id[j], name[j], days_off[j], user_id, createdDate[j]);
                temporary.Logs.SetTheLog(logs[j], log_count[j], longest_streak[j], current_streak[j]);
                hab.Add(temporary);
            }

            return hab;
        }
    }
}