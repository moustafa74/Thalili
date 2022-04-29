using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;

namespace Thalili.Controllers
{
    public class Analysis_and_LabsController : Controller
    {
        // GET: Analysis_and_Labs
        thaliliEntities context = new thaliliEntities();
        public ActionResult Index(int? page)
        {
            var labs = context.labs.ToList();
            var anlysis = context.medical_analysis.ToList();
            search_view anlysis_labs = new search_view(labs, anlysis);
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            return View("lab_analysis", anlysis_labs);
        }
        public ActionResult lab(int id)
        {
            return View();
        }
        public ActionResult analysis(int id)
        {
            return View();
        }
    }
    
}