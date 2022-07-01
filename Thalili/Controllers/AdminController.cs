using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Thalili.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Current_orders()
        {
            return View();
        }
        public ActionResult Labs()
        {
            return View();
        }
        public ActionResult Analysis()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
    }
}