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
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            var labs = Context.labs.ToList();
            var requst = Context.requests.ToList();
            AdminData labs_requst = new AdminData(labs, requst);
            return View(labs_requst);
        }
        public ActionResult RefuseRequest(int id)
        {
            var request = Context.requests.Where(d => d.requst_ID == id).FirstOrDefault();
            if(request != null)
            {
                Context.requests.Remove(request);
                Context.SaveChanges();
            }
            return RedirectToAction("Labs");
        }
        public ActionResult Analysis()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}