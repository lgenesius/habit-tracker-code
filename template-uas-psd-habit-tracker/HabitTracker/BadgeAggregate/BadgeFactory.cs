using System;
using System.Collections.Generic;
using HabitTracker.BadgeCollector;

using Npgsql;
using HabitTracker.Database.Postgresql;
using System.Linq;


namespace HabitTracker{
    public class BadgeFactory{
        public void CreateFirstBadge(IBadgeCollector firstCollect, List<_Badge> _badge, Guid user_id){
            PostgreConnectRepo connect = new PostgreConnectRepo();
            string name = firstCollect.Collect()[0];
            string description = firstCollect.Collect()[1];
            _Badge bdg = new _Badge(Guid.NewGuid(),name, description, user_id, DateTime.Now);
            if(connect.UserRepo.FindById(user_id) != null){
                connect.BadgeRepo.Create(bdg);
                connect.Commit();
            }
            _badge.Add(bdg);
        }

        public void CreateSecondBadge(IBadgeCollector secondCollect, List<_Badge> _badge, Guid user_id){
            PostgreConnectRepo connect = new PostgreConnectRepo();
            string name = secondCollect.Collect()[0];
            string description = secondCollect.Collect()[1];          
            _Badge bdg = new _Badge(Guid.NewGuid(), name, description, user_id, DateTime.Now);
            connect.BadgeRepo.Create(bdg);
            connect.Commit();
            _badge.Add(bdg);
        }

        public void CreateThirdBadge(IBadgeCollector thirdCollect, List<_Badge> _badge, Guid user_id){
            PostgreConnectRepo connect = new PostgreConnectRepo();
            string name = thirdCollect.Collect()[0];
            string description = thirdCollect.Collect()[1];
            _Badge bdg = new _Badge(Guid.NewGuid(), name, description, user_id, DateTime.Now);
            connect.BadgeRepo.Create(bdg);
            connect.Commit();
            _badge.Add(bdg);
        }
    }
}