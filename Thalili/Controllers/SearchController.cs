using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;
namespace Thalili.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        thaliliEntities context = new thaliliEntities();
        public ActionResult Index(string searchField,int? page)
        {
            //if(List=="all")
            //{
            //    //ViewData["data"] = labs;
            //    //(IEnumerable<lab>)ViewData["labs"]
            //}
            var labs = context.labs.Where(d => d.name.Contains(searchField)).ToList();
            var analyss= context.medical_analysis.Where(d => d.name.Contains(searchField)).ToList();
            search_view anal_lab = new search_view(labs,analyss);
            ViewData["searchField"] = searchField;
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            return View("lab_analysis",anal_lab);
        }
        //public ActionResult filters(string searchField, int typeofsearch, int price, int rating)
        //{
        //    search_view result;
        //    ViewData["searchField"] = searchField;
        //    ViewData["page"] = 1;
        //    if (typeofsearch == 1)
        //    {
        //        result =
        //            }


        //}
    }
}