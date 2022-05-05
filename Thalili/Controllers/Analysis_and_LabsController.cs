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
        public ActionResult analysis(int id, int? page)
        {
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            var labs = context.analysis_in_lab.Where(d => d.medical_analysis_id == id).ToList();
            return View(labs);
        }
        public ActionResult AddToCart(int lab_id, int analysis_id)
        {

            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Login");
            int user_id = (int)Session["UserID"];
            cart cart = new cart();
            cart.analysis_id = analysis_id;
            cart.Lab_id = lab_id;
            cart.user_id = user_id;
            cart.count = 1;
            context.carts.Add(cart);
            context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult SearchLab(int AnalysisID, string Labname, int? page)
        {
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            var analysis_in_lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == AnalysisID && d.lab.name.Contains(Labname)).ToList();
            return View("analysis",analysis_in_lab);
        }
        
        public ActionResult Labsfilter (int filterPrice , int filterRating , int? page , int analysis_id)
        {
            var lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id).ToList();
            int from = 0, to = 0;
            if (page == null)
                page = 1;
            
            ViewData["page"] = page;

            if(filterPrice == 1)
            {
                 lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id).ToList();
                from = 0;
            }
            else if(filterPrice == 2)
            {
               // var lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.price >= 50 && d.price <= 100).ToList();
                from = 50; to = 100;
            }
            else if (filterPrice == 3)
            {

                 lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.price >= 100 && d.price <= 200).ToList();
            }
            else if (filterPrice == 4)
            {
                 lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.price >= 200 && d.price <= 300).ToList();
            }
            else if (filterPrice == 5)
            {
                 lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.price >= 300).ToList();
            }

            if(filterRating == 5)
            {
                 lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.lab.lab_rating <= 5 && d.price >= from && d.price <= to).ToList();
            }
            //if (filterRating == 1)
            //{
            //    var lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.lab.lab_rating <= 1 && d.price >= from && d.price <= to).ToList();
            //}
            //if (filterRating == 1)
            //{
            //    var lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.lab.lab_rating <= 1 && d.price >= from && d.price <= to).ToList();
            //}
            //if (filterRating == 1)
            //{
            //    var lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.lab.lab_rating <= 1 && d.price >= from && d.price <= to).ToList();
            //}
            //if (filterRating == 1)
            //{
            //    var lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.lab.lab_rating <= 1 && d.price >= from && d.price <= to).ToList();
            //}
            //if (filterRating == 1)
            //{
            //    var lab = context.analysis_in_lab.Where(d => d.medical_analysis_id == analysis_id && d.lab.lab_rating <= 1 && d.price >= from && d.price <= to).ToList();
            //}
            return View("analysis", lab);

        }
    }

}