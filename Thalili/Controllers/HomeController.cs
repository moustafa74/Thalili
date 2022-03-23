using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thalili.Models;

namespace Thalili.Controllers
{

    public class HomeController : Controller
    {
        thaliliEntities Context = new thaliliEntities();

        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult test(uploadFile upl)
        //{
        //    //result r = new result();
        //    //r.Labs_id = 1;
        //    //r.medical_analysis_id = 1;
        //    //r.user_id = 1;
        //    ////var s = Base64Encode(pd);
        //    //r.pdf = pd;
        //    result res = new result();
        //    var fileExtenstion = Path.GetExtension(upl.file.FileName);
        //    var fileguid = Guid.NewGuid().ToString();
        //    res.medical_analysis_id = 1;
        //    res.Labs_id = 1;
        //    res.user_id = 1;
        //    res.pdf = fileguid + fileExtenstion;
        //    string filePath = Server.MapPath($"~/Content/assets/imgs/{res.pdf}");
        //    Context.results.Add(res);
        //    Context.SaveChanges();
        //    upl.file.SaveAs(filePath);
        //    return RedirectToAction("Index");
        //}//
    }
}