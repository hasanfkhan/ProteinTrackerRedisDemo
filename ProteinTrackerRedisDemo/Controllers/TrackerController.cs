using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack;
using ServiceStack.Redis;
using ProteinTrackerRedisDemo.Models;

namespace ProteinTrackerRedisDemo.Controllers
{
    public class TrackerController : Controller
    {
        //
        // GET: /Tracker/

        public ActionResult Index(long userId, int amount = 0)
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.As<User>();
                var user = userClient.GetById(userId);

                if (amount > 0)
                {
                    user.Total += amount;
                    userClient.Store(user);
                }

                ViewBag.Id = user.Id;
                ViewBag.Name = user.Name;
                ViewBag.Total = user.Total;
                ViewBag.Goal = user.Goal;

            }
            return View();
        }

    }
}
