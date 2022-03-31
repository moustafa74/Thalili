using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Thalili.Controllers
{
    public class LabDashboardController : Controller
    {
        public ActionResult Orders()
        {
            return View();
        }
        public ActionResult Analysis()
        {
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }
    }
}