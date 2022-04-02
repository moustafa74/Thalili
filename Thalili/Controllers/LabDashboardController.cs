using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;

namespace Thalili.Controllers
{
    public class LabDashboardController : Controller
    {
        thaliliEntities context = new thaliliEntities();
        int lab_id = 1;
        public ActionResult Orders()
        {
            return View();
        }
        public ActionResult Analysis()
        {

            var analysis_in_lab = context.analysis_in_lab.Where(d => d.Labs_id == lab_id).ToList();

            return View(analysis_in_lab);
        }
        public ActionResult EditThalil(string tName, decimal price)
        {
            var edit = context.analysis_in_lab.FirstOrDefault(d => d.Labs_id == lab_id && d.medical_analysis.name == tName);
            edit.price = price;
            context.SaveChanges();
            return View("Orders");
        }
        public ActionResult Settings()
        {
            return View();
        }
    }
}