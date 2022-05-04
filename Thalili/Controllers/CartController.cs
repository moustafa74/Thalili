using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;

namespace Thalili.Controllers
{
    public class CartController : Controller
    {
        thaliliEntities context = new thaliliEntities();
        // GET: Cart
        public ActionResult Index()
        {
            if(Session["UserID"]==null)
                return RedirectToAction("Index", "Login");
            int user_id =(int) Session["UserID"];
            var cart = context.carts.Where(d => d.user_id == user_id).ToList();
            return View(cart);
        }
        public ActionResult edit(int analysis_id, int Lab_id,int count)
        {

            return RedirectToAction("Index");
        }
        public ActionResult deleteItem(int analysis_id,int Lab_id)
        {
            return RedirectToAction("Index");
        }
        public ActionResult completeRequst(string Adress)
        {
            int user_id = (int)Session["UserID"];
            List<sub_order> suborders = new List<sub_order>();
            order order = new order();
            order.location = Adress;
            order.date = DateTime.Now;   
            context.orders.Add(order);
            var carts = context.carts.Where(d => d.user_id == user_id).ToList();
            foreach (cart crt in carts)
            {
                sub_order sub_order = new sub_order();
                sub_order.user_id = user_id;
                sub_order.medical_analysis_id = crt.analysis_id;
                sub_order.lab_id = crt.Lab_id;
                sub_order.count = crt.count;
                order.sub_order.Add(sub_order);
                context.carts.Remove(crt);
            }
            context.SaveChanges();
            return View();
        }
    }
}