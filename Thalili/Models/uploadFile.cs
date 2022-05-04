using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thalili.Models
{
    public class uploadFile
    {
        public string file_name;
        public HttpPostedFileBase file { get; set; }
    }
}