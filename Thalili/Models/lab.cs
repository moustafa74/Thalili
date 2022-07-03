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
    
    public partial class lab
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lab()
        {
            this.analysis_in_lab = new HashSet<analysis_in_lab>();
            this.reviews = new HashSet<review>();
        }
    
        public int lab_id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public Nullable<decimal> lab_rating { get; set; }
        public string img { get; set; }
        public string phone_number { get; set; }
        public bool is_available { get; set; }
        public Nullable<int> lab_owner_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<analysis_in_lab> analysis_in_lab { get; set; }
        public virtual lab_owner lab_owner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<review> reviews { get; set; }
    }
}
