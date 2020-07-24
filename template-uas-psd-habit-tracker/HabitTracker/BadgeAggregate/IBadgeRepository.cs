using System;
using System.Collections.Generic;

namespace HabitTracker{
    public interface IBadgeRepository{
        _Badge FindByUserId(Guid user_id);
        void Create(_Badge badge);
        List<_Badge> GetAllBadge(Guid user_id);
    }
}