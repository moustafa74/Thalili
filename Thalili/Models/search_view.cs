using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thalili.Models
{
    public class search_view
    {
    public List<lab> labList { get; set; }
    public List<medical_analysis> analysisList { get; set; }

        public search_view() { }
       

        public search_view(List<lab> lablist, List<medical_analysis> analysisList)
        {
            this.labList = lablist;
            this.analysisList = analysisList;
        }
    }
}