using System.Web.Mvc;
using System.Web.Routing;
using ProteinTrackerRedisDemo.Models;
using ServiceStack.Redis;

namespace ProteinTrackerRedisDemo.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult NewUser()
        {
            return View();
        }

        public ActionResult Save(string name, int goal, long? userId)
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.As<User>();

                User user;
                if (userId != null)
                {
                    user = userClient.GetById(userId);
                }
                else
                {
                    user = new User
                    {
                       Id = userClient.GetNextSequence()
                    };
                }

                user.Name = name;
                user.Goal = goal;
                userClient.Store(user);
                userId = user.Id;
            }
            return RedirectToAction("Index", "Tracker", new { userId });
        }

        public ActionResult Edit(long userId)
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.As<User>();
                var user = userClient.GetById(userId);
                ViewBag.Id = user.Id;
                ViewBag.Name = user.Name;
                ViewBag.Goal = user.Goal;
            }
            return View("NewUser");
        }
    }
}