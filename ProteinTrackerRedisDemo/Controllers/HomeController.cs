using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Redis;
using ProteinTrackerRedisDemo.Models;

namespace ProteinTrackerRedisDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.As<User>();
                var users = userClient.GetAll();
                var userSelection = new SelectList(users, "Id", "Name", String.Empty);
                ViewBag.UserId = userSelection;
            }

            return View();
        }

    }
}
