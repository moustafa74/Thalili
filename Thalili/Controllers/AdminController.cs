using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thalili.Models;
using System.Web.Mvc;

namespace Thalili.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ThaliliEntities Context = new ThaliliEntities();

        public ActionResult Login(admin admin)
        {
            return View();
        }
        public ActionResult LoginConfirm(admin admin)
        {
            string crc = admin.pass;//Crypto.Hash(user.pass);
            var AdminDetail = Context.admins.Where(x => x.email == admin.email && x.pass == crc).FirstOrDefault();
            ViewData["ERoor"] = "";
            if (AdminDetail == null)
            {
                ViewData["ERoor"] = "البريد الالكترونى او الرقم السرى غير صحيح";
                ViewData["Email"] = admin.email;
                ViewData["Password"] = admin.pass;
                return View("Login");
            }
            else
            {
                Session["AdminID"] = AdminDetail.admin1;
                Session["AdminName"] = AdminDetail.name;
                return RedirectToAction("Labs", "Admin");
            }
        }
        public ActionResult Current_orders()
        {
            return View();
        }
        public ActionResult Labs(int ? page)
        {
            //if (Session["AdminID"] == null)
            //    return RedirectToAction("Login");
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            var labs = Context.labs.ToList();
            var requst = Context.requests.ToList();
            AdminData labs_requst = new AdminData(labs, requst);
            return View(labs_requst);
        }
        public ActionResult RefuseRequest(int Request_id)
        {
            var request = Context.requests.Where(d => d.requst_ID == Request_id).FirstOrDefault();
            if(request != null)
            {
                Context.requests.Remove(request);
                Context.SaveChanges();
            }
            return RedirectToAction("Labs");
        }
        public ActionResult Analysis(int? page)
        {
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            var analysis = Context.medical_analysis.ToList();
            return View(analysis);
        }
        public ActionResult DeleteAnalysis(int Delete_id)
        {
            var analysiss = Context.medical_analysis.Where(d => d.medical_analysis_id == Delete_id).FirstOrDefault();
            if (analysiss != null)
            {
                Context.medical_analysis.Remove(analysiss);
                Context.SaveChanges();
            }
            return RedirectToAction("Analysis");
        }
        public ActionResult Users(int? page )
        {
            
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            var user = Context.users.ToList();
            return View(user);
        }
        public ActionResult DeleteUser(int user_id)
        {
             var deleteduser = Context.users.Where(d => d.user_id == user_id).FirstOrDefault();
            if (deleteduser != null)
            {
                Context.users.Remove(deleteduser);
                Context.SaveChanges();
            }
            return RedirectToAction("Users");
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult AddAnalysis()
        {
            var smple = Context.samples.ToList();
            return View(smple);
        }
        public ActionResult Save_Analysis(AnalysisModel analysis)
        {
            medical_analysis ma = new medical_analysis();
            ma.name = analysis.name;
            ma.img = analysis.img;
            ma.description = analysis.description;
            var sample = Context.samples.Where(d => d.sample_name == analysis.sample).ToList();
            ma.samples = sample;
            Context.medical_analysis.Add(ma);
            Context.SaveChanges();

            return RedirectToAction("Analysis");
        }
    }
}