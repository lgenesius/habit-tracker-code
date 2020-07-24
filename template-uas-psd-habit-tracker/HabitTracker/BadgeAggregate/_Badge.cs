using System;
using System.Collections.Generic;

namespace HabitTracker
{
    public class _Badge
    {
        private Guid id;
        private string name;
        private string description;
        private Guid user_id;
        private DateTime created_at;

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

        public string Description
        {
            get
            {
                return description;
            }
        }

        public Guid User_Id{
            get{
                return user_id;
            }
        }

        public DateTime Created_At{
            get{
                return created_at;
            }
        }

        public _Badge(Guid id, string name, string description, Guid user_id, DateTime created_at)
        {
            if (checkNameAndDescription(name, description) == false)
            {
                throw new Exception("The name and/or description is wrong, Please check it again!");
            }
            this.id = id;
            this.name = name;
            this.description = description;
            this.user_id = user_id;
            this.created_at = created_at;
        }

        private bool checkNameAndDescription(string name, string description)
        {
            bool check = false;
            if (name == "Dominating" && description == "4+ streak" || name != "Workaholic" || name != "Epic Comeback")
            {
                check = true;
            }
            else if(name == "Workaholic" && description == "Doing some works on days-off"){
                check = true;
            }
            else if(name == "Epic Comeback" && description == "10 streak after 10 days without logging"){
                check = true;
            }

            return check;
        }

        public override bool Equals(object obj){
            var badge = obj as _Badge;
            if(badge == null) return false;

            return this.id == badge.id;
        }

        public override int GetHashCode(){
            return base.GetHashCode();
        }
    }
}