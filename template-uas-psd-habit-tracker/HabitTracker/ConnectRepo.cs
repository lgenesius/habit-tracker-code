using System;

namespace HabitTracker{
    public interface ConnectRepo : IDisposable{
        void Rollback();
        void Commit();
    }
}