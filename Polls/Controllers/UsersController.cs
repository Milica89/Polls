using Polls.DAL;
using Polls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polls.Controllers
{
    public class UsersController : Controller
    {
        public PollsContext db = new PollsContext();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}
