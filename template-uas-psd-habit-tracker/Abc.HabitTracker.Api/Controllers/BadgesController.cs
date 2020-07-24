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
    public class BadgesController : ControllerBase
    {
        private readonly ILogger<BadgesController> _logger;

        public BadgesController(ILogger<BadgesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("api/v1/users/{userID}/badges")]
        public ActionResult<IEnumerable<Badge>> All(Guid userID)
        {
            PostgreConnectRepo connect = new PostgreConnectRepo();
            List<Badge> badg = new List<Badge>();

            if (connect.UserRepo.FindById(userID) == null)
            {
                throw new Exception("Does not have that badge with that userID");
            }

            User user_people = connect.UserRepo.FindById(userID);

            if (connect.BadgeRepo.FindByUserId(userID) == null)
            {
                throw new Exception("Does not have that badge with that userID");
            }

            foreach(var el in connect.BadgeRepo.GetAllBadge(userID)){
                badg.Add(new Badge(el.ID, el.Name, el.Description, el.User_Id, el.Created_At));
            }
            
            return badg;
        }
    }
}
