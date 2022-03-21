using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;

namespace Thalili.Controllers
{

    public class HomeController : Controller
    {
        thaliliEntities1 Context = new thaliliEntities1();
        
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult serc()
        {
            return View();
        }
    }
}