using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;
using Thalili.Helpers;
namespace Thalili.Controllers
{
    public class LoginController : Controller
    {
        thaliliEntities Context = new thaliliEntities();
        // GET: Login     
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginConfirm(user user)
        {
            string crc = Crypto.Hash(user.pass); 
            var userDetail = Context.users.Where(x => x.email == user.email && x.pass == crc).FirstOrDefault();
            if (userDetail == null)
            {
                ViewData["ERoor"] = "البريد الالكترونى او الرقم السرى غير صحيح";
                ViewData["Email"] = user.email;
                ViewData["Password"] = user.pass;
                return View("index");
            }
            else
            {
                Session["UserID"] = userDetail.user_id;
                Session["userName"] = userDetail.name;
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        public ActionResult SetPassword(string id)
        {
            var acc = Context.users.Where(a => a.reset_pass_ac == id).FirstOrDefault();
            return View(acc);
        }

        public ActionResult ConfirmPassword(string id,string pass)
        {
            var acc = Context.users.Where(a => a.reset_pass_ac == id).FirstOrDefault();
            acc.pass = Crypto.Hash(pass);
            Context.SaveChanges();
            return RedirectToAction("Index","Login");
        }
        [HttpPost]
        public ActionResult ResetPasswordOR(string emailField)
        {
            var acc = Context.users.Where(x => x.email == emailField).FirstOrDefault();
            if(acc==null)
            {
                ViewBag.ERooor = "البريد الالكترونى  غير مستخدم";
                ViewBag.Emaill = emailField;
                return View("ResetPassword");

            }
            acc.reset_pass_ac= Guid.NewGuid().ToString();
            Context.SaveChanges();
            SendVerificationLinkEmail(emailField, acc.reset_pass_ac);
            return View();
        }
        public void SendVerificationLinkEmail(string email, string reset_pass_ac)
        {
            var resetUrl = "/Login/SetPassword/" + reset_pass_ac;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, resetUrl);

            var fromEmail = new MailAddress("tthalyly@gmail.com", "Thalili");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "mama2468"; // Replace with actual password
            string subject = "Reset Password!";

            string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
            " successfully created. Please click on the below link to verify your account" +
            " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)};

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}