﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

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
            
            var AllOrder = Context.sub_order.ToList();
            return View(AllOrder);
            
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
        public ActionResult AcceptRequest(int Request_id)
        {
            var request = Context.requests.Where(d => d.requst_ID == Request_id).FirstOrDefault();
            lab_owner lbowner = new lab_owner();
            lbowner.email = request.email;
            lbowner.name = request.owner_name;
            lbowner.pass = Membership.GeneratePassword(8, 2);
            Context.lab_owner.Add(lbowner);
            lab lab1 = new lab();
            lab1.name = request.name;
            lab1.location = request.location;
            lab1.phone_number = request.phone_number;
            lab1.is_available = false;
            lab1.lab_owner_id = lbowner.lab_owner_id;
            Context.labs.Add(lab1);
            Context.requests.Remove(request);
            Context.SaveChanges();
            SendAcceptEmail(lbowner.email, lbowner.pass);
            return RedirectToAction("Labs");
        }
        public void SendAcceptEmail(string email, string pass)
        {
            var fromEmail = new MailAddress("tthalyly@gmail.com", "Thalili");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "mama2468"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "تهانينا!<br/>نود اخباركم انه تم قبول طلبكم للانضمام الي قائمة شركاء النجاح لدينا ونحن سعيدين ولا نستطيع الانتظار لكي نري ما يمكننا ان نحققه سويا.<br/>هذه هي بيانات الدخول الخاصة بكم - يمكنكم البدء في الحال بالدخول الي لوحة التحكم واضافة وتعديل كل ما يقدمه معملكم للجمهور<br/>البريد الالكترونى : " + email + "<br/>كلمة السر :" + pass + "<br/>نود تذكيركم ايضا انه يمكنم التواصل مع الدعم الفني لدينا في اي وقت علي الارقام وقنوات التواصل التالية في حال كان لديكم اي استفسار او اي شئ غير مفهوم في النظام البرمجي للموقع.<br/><br/>سعداء بوجودكم بعائلة تحاليلي ونتطلع للعمل سويا في اقرب وقت.";
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult RefuseRequest(int Request_id)
        {
            var request = Context.requests.Where(d => d.requst_ID == Request_id).FirstOrDefault();
            if(request != null)
            {
                Context.requests.Remove(request);
                Context.SaveChanges();
                SendRefuseEmail(request.email);
            }
            return RedirectToAction("Labs");
        }
        public void SendRefuseEmail(string email)
        {
            var fromEmail = new MailAddress("tthalyly@gmail.com", "Thalili");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "mama2468"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "";
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult Analysis(int? page)
        {
            if (page == null)
                page = 1;
            ViewData["page"] = page;
            var analysis = Context.medical_analysis.ToList();
            return View(analysis);
        }
        public ActionResult EditAnalysis(int id)
        {
            return View();
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

        public ActionResult Available_Analysis(int id, int? page)
        {

            //if (Session["AdminID"] == null)
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            if (page == null)
                page = 1;
            ViewData["page"] = page;

            var analysis = Context.analysis_in_lab.Where(d => d.Labs_id == id).ToList();

            return View(analysis);

        }

        public ActionResult AddAnalysis()
        {
            var smple = Context.samples.ToList();
            return View(smple);
        }
        //public ActionResult Save_Analysis(AnalysisModel analysis)
        //{
        //    medical_analysis ma = new medical_analysis();
        //    ma.name = analysis.name;
        //    ma.img = analysis.img;
        //    ma.description = analysis.description;
        //    var sample = Context.samples.Where(d => d.sample_name == analysis.sample).ToList();
        //    ma.samples = sample;

        //    var fileExtenstion = Path.GetExtension(analysis.File.FileName);
        //    var fileguid = Guid.NewGuid().ToString();
        //    var filee = fileguid + fileExtenstion;
        //    string filePath = Server.MapPath($"~/Content/Images/Analysis/{filee}");
        //    analysis.File.SaveAs(filePath);
        //    ma.img = filee;
        //    Context.medical_analysis.Add(ma);
        //    Context.SaveChanges();
        //    return RedirectToAction("Analysis");
        //}


    }
}