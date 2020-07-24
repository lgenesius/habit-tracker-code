using System;
using System.Collections.Generic;

namespace HabitTracker
{
    public class User
    {
        private Guid id;
        private string name;

        private int total_log_user;
        private List<_Badge> _badge;

        public Guid ID
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int Total_Log_User{
            get{
                return total_log_user;
            }
        }

        public void AddTotalLogUser(){
            this.total_log_user++;
        }

        public List<_Badge> _Badge{
            get{
                return _badge;
            }
        }

        public User(Guid id, string name)
        {
            this.id = id;
            this.name = name;
            this.total_log_user = 0;
            _badge = new List<_Badge>();
        }

        public void AddBadge(_Badge bdg){
            if(bdg == null){
                throw new Exception("Must input the badge!");
            }

            if(_badge.Count == 3){
                throw new Exception("Already have three badges");
            }

            _badge.Add(bdg);
        }

        public static User NewUser(Guid id, string name){
            return new User(id, name);
        }

        public override bool Equals(object obj){
            var user = obj as User;
            if(user==null) return false;

            return this.id == user.id;
        }

        public override int GetHashCode(){
            return base.GetHashCode();
        }
    }
}
