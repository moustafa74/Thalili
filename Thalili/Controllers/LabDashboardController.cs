using System;
using System.Collections.Generic;
using System.IO;
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
            var sub = context.sub_order.Where(d => d.analysis_in_lab.Labs_id == lab_id).ToList();
            return View(sub);
        }
        public ActionResult Analysis()
        {
                
            var analysis_in_lab = context.analysis_in_lab.Where(d => d.Labs_id == lab_id).ToList();

            return View(analysis_in_lab);
        }
        public ActionResult EditThalil(string ThalilName, decimal price)
        {
            var edit = context.analysis_in_lab.Where(d => d.Labs_id == lab_id && d.medical_analysis.name == ThalilName).FirstOrDefault();
            edit.price = price;
            context.SaveChanges();
            return RedirectToAction("Analysis");
        }

        public ActionResult AddThalil(string ThalilName, decimal price)
        {
            try
            {            
                var analysis = context.medical_analysis.Where(d => d.name == ThalilName).FirstOrDefault();
                analysis_in_lab New_Analysis = new analysis_in_lab();
                New_Analysis.Labs_id = lab_id;
                New_Analysis.medical_analysis_id = analysis.medical_analysis_id;
                New_Analysis.price = price;
                context.analysis_in_lab.Add(New_Analysis);
                context.SaveChanges();

                return RedirectToAction("Analysis");
            }
            catch(Exception e)
            {
                TempData["msg"] = "<script>alert('Enter valid Analysis name');</script>";
                return RedirectToAction("Analysis");
            }
        }
        public ActionResult DeleteThalil(int id)
        {
            var deleteitem = context.analysis_in_lab.Where(d => d.Labs_id == lab_id && d.medical_analysis_id == id).FirstOrDefault();
            context.analysis_in_lab.Remove(deleteitem);
            context.SaveChanges();
            return RedirectToAction("Analysis");
        }

        [HttpGet]
        public ActionResult Settings()
        {
            var lab = context.labs.Where(d => d.lab_id == lab_id).FirstOrDefault();

            return View(lab);
        }

        [HttpPost]
        public ActionResult Settings(lab edits)
        {
            var lab = context.labs.Where(d => d.lab_id == lab_id).FirstOrDefault();
            lab.name = edits.name;
            lab.phone_number = edits.phone_number;
            lab.description = edits.description;
            lab.location = edits.location;
            lab.lab_owner.email = edits.lab_owner.email;
            lab.lab_owner.pass = edits.lab_owner.pass;
            lab.img = edits.img;
            context.SaveChanges();

            return RedirectToAction("Settings");
        }
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            var fileExtenstion = Path.GetExtension(file.FileName);
            var fileguid = Guid.NewGuid().ToString();
            var filee = fileguid + fileExtenstion;
            string filePath = Server.MapPath($"~/Content/Images/{filee}");
            TempData["Image"] = file;
            file.SaveAs(filePath);

            return RedirectToAction("Settings");

        }
    }
}