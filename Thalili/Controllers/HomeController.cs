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
        //public ActionResult Search(string searchName)
        //{
        //    var anal_lab = Context.analysis_in_lab.Where(d => d.medical_analysis.name.Contains(searchName) || d.lab.name.Contains(searchName)).ToList();
        //    return View("SearchResult", anal_lab);
        //}
    }
}