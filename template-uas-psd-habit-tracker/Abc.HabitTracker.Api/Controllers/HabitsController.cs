using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Npgsql;
using HabitTracker.Database.Postgresql;
using HabitTracker;

namespace Abc.HabitTracker.Api.Controllers
{
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly ILogger<HabitsController> _logger;

        public HabitsController(ILogger<HabitsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("api/v1/users/{userID}/habits")]
        public ActionResult<IEnumerable<Habit>> All(Guid userID)
        {
            PostgreConnectRepo connect = new PostgreConnectRepo();

            Habit hab = new Habit();

            List<Habit> listhab = new List<Habit>();

            if (connect.UserRepo.FindById(userID) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }
            
            foreach(var el in connect.HabitRepo.GetAllHabit(userID)){
                hab.ID = el.ID;
                hab.Name = el.Name;
                hab.DaysOff = el.Days_Off.Days;
                hab.CurrentStreak = el.Logs.Current_Streak;
                hab.LongestStreak = el.Logs.Longest_Streak;
                hab.LogCount = el.Logs.Log_Count;
                hab.Logs = el.Logs.Logs;
                hab.UserID = el.userID;
                hab.CreatedAt = el.Created_At;
                listhab.Add(hab);
            }

            return listhab;
        }

        [HttpGet("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<Habit> Get(Guid userID, Guid id)
        {
            PostgreConnectRepo connect = new PostgreConnectRepo();
            Habit hbt = new Habit();

            if (connect.UserRepo.FindById(userID) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }

            if (connect.HabitRepo.FindByUserId(userID, id) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }

            _Habit foo = connect.HabitRepo.FindByUserId(userID, id);

            hbt.ID = foo.ID;
            hbt.Name = foo.Name;
            hbt.DaysOff = foo.Days_Off.Days;
            hbt.CurrentStreak = foo.Logs.Current_Streak;
            hbt.LongestStreak = foo.Logs.Longest_Streak;
            hbt.LogCount = foo.Logs.Log_Count;
            hbt.Logs = foo.Logs.Logs;
            hbt.UserID = foo.userID;
            hbt.CreatedAt = foo.Created_At;
            return hbt;
        }

        [HttpPost("api/v1/users/{userID}/habits")]
        public ActionResult<Habit> AddNewHabit(Guid userID, [FromBody] RequestData data)
        {
            PostgreConnectRepo connect = new PostgreConnectRepo();
            Habit hbt = new Habit();
            string[] arr = { "Mon", "Wed", "Fri" };
            if(data.DaysOff != null){
                arr = data.DaysOff;
            }

            if (connect.UserRepo.FindById(userID) == null)
            {
                User us = new User(userID, "Budi");
                connect.UserRepo.Create(us);
            }

            _Habit b = _Habit.NewHabit(Guid.NewGuid(), data.Name, arr, userID, DateTime.Now);
            connect.HabitRepo.Create(b);
            connect.Commit();

            _Habit foo = connect.HabitRepo.FindByUserId(userID, b.ID);

            hbt.ID = foo.ID;
            hbt.Name = foo.Name;
            hbt.DaysOff = foo.Days_Off.Days;
            hbt.CurrentStreak = foo.Logs.Current_Streak;
            hbt.LongestStreak = foo.Logs.Longest_Streak;
            hbt.LogCount = foo.Logs.Log_Count;
            hbt.Logs = foo.Logs.Logs;
            hbt.UserID = foo.userID;
            hbt.CreatedAt = foo.Created_At;
            return hbt;
        }

        [HttpPut("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<Habit> UpdateHabit(Guid userID, Guid id, [FromBody] RequestData data)
        {
            PostgreConnectRepo connect = new PostgreConnectRepo();
            Habit hbt = new Habit();

            if (connect.UserRepo.FindById(userID) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }

            if (connect.HabitRepo.FindByUserId(userID, id) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }

            if(data.DaysOff == null){
                connect.HabitRepo.UpdateName(id, data.Name, userID);
                connect.Commit();
            }
            else{
                connect.HabitRepo.UpdateNameAndDays(id, data.Name, data.DaysOff, userID);
                connect.Commit();
            }

            _Habit foo = connect.HabitRepo.FindByUserId(userID, id);
            hbt.ID = foo.ID;
            hbt.Name = foo.Name;
            hbt.DaysOff = foo.Days_Off.Days;
            hbt.CurrentStreak = foo.Logs.Current_Streak;
            hbt.LongestStreak = foo.Logs.Longest_Streak;
            hbt.LogCount = foo.Logs.Log_Count;
            hbt.Logs = foo.Logs.Logs;
            hbt.UserID = foo.userID;
            hbt.CreatedAt = foo.Created_At;
            return hbt;

        }

        [HttpDelete("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<Habit> DeleteHabit(Guid userID, Guid id)
        {
            PostgreConnectRepo connect = new PostgreConnectRepo();
            Habit hbt = new Habit();

            if (connect.UserRepo.FindById(userID) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }

            if (connect.HabitRepo.FindByUserId(userID, id) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }

            _Habit foo = connect.HabitRepo.FindByUserId(userID, id);
            hbt.ID = foo.ID;
            hbt.Name = foo.Name;
            hbt.DaysOff = foo.Days_Off.Days;
            hbt.CurrentStreak = foo.Logs.Current_Streak;
            hbt.LongestStreak = foo.Logs.Longest_Streak;
            hbt.LogCount = foo.Logs.Log_Count;
            hbt.Logs = foo.Logs.Logs;
            hbt.UserID = foo.userID;
            hbt.CreatedAt = foo.Created_At;

            connect.HabitRepo.DeleteHabit(id, userID);
            connect.Commit();

            return hbt;
        }

        [HttpPost("api/v1/users/{userID}/habits/{id}/logs")]
        public ActionResult<Habit> Log(Guid userID, Guid id)
        {

            PostgreConnectRepo connect = new PostgreConnectRepo();
            Habit hbt = new Habit();
            User us;
            _Habit foo;

            if (connect.UserRepo.FindById(userID) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }
            else
            {
                us = connect.UserRepo.FindById(userID);
            }

            if (connect.HabitRepo.FindByUserId(userID, id) == null)
            {
                throw new Exception("Does not have that Habit with that userID");
            }
            else
            {
                foo = connect.HabitRepo.FindByUserId(userID, id);
                foo.doHabit(foo.Days_Off.Days[0], us);
                foo.doHabit(foo.Days_Off.Days[0], us);
                foo.doHabit(foo.Days_Off.Days[0], us);
                foo.doHabit(foo.Days_Off.Days[0], us);
                foo.doHabit(foo.Days_Off.Days[0], us);

                connect.HabitRepo.UpdateLog(id, foo);
                connect.Commit();
                _Habit boo = connect.HabitRepo.FindByUserId(userID, id);
                hbt.ID = boo.ID;
                hbt.Name = boo.Name;
                hbt.DaysOff = boo.Days_Off.Days;
                hbt.CurrentStreak = boo.Logs.Current_Streak;
                hbt.LongestStreak = boo.Logs.Longest_Streak;
                hbt.LogCount = boo.Logs.Log_Count;
                hbt.Logs = boo.Logs.Logs;
                hbt.UserID = boo.userID;
                hbt.CreatedAt = boo.Created_At;
            }
            return hbt;
        }

        //mock data only. remove later
        private static readonly Guid AmirID = Guid.Parse("4fbb54f1-f340-441e-9e57-892329464d56");
        private static readonly Guid BudiID = Guid.Parse("0b54c1fe-a374-4df8-ba9a-0aa7744a4531");

        //mock data only. remove later
        private static readonly Habit habitAmir1 = new Habit
        {
            ID = Guid.Parse("fd725b05-a221-461a-973c-4a0899cee14d"),
            Name = "baca buku",
            UserID = AmirID
        };

        //mock data only. remove later
        private static readonly Habit habitAmir2 = new Habit
        {
            ID = Guid.Parse("01169031-752e-4c52-822c-a04d290438ea"),
            Name = "code one simple app prototype",
            DaysOff = new[] { "Sat", "Sun" },
            UserID = AmirID
        };

        //mock data only. remove later
        private static readonly Habit habitBudi1 = new Habit
        {
            ID = Guid.Parse("05fb5a61-aa1f-4a96-b952-378bf73ca713"),
            Name = "100 push-ups, 100 sit-ups, 100 squats",
            LongestStreak = 100,
            CurrentStreak = 10,
            LogCount = 123,
            UserID = BudiID
        };
    }
}
