﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class thaliliEntities1 : DbContext
    {
        public thaliliEntities1()
            : base("name=thaliliEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<analysis_in_lab> analysis_in_lab { get; set; }
        public virtual DbSet<lab> labs { get; set; }
        public virtual DbSet<lab_owner> lab_owner { get; set; }
        public virtual DbSet<medical_analysis> medical_analysis { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<request> requests { get; set; }
        public virtual DbSet<result> results { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<sample> samples { get; set; }
        public virtual DbSet<tag> tags { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}