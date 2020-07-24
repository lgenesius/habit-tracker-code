using System;
using System.Collections.Generic;

namespace HabitTracker{
    public interface IHabitRepository{
        _Habit FindByUserId(Guid user_id, Guid id);
        void Create(_Habit habit);
        void UpdateName(Guid id, string _name, Guid userId);

        void UpdateNameAndDays(Guid id, string _name, string[] days_off, Guid userId);

        void DeleteHabit(Guid id, Guid userId);
        void UpdateLog(Guid id, _Habit habit);

        List<_Habit> GetAllHabit(Guid userId);
    }
}