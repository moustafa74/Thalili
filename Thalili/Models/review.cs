//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Thalili.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class review
    {
        public int user_id { get; set; }
        public int Labs_id { get; set; }
        public string comment { get; set; }
        public Nullable<decimal> rating { get; set; }
    
        public virtual lab lab { get; set; }
        public virtual user user { get; set; }
    }
}
