﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Product_Master.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProductMasterEntities : DbContext
    {
        public ProductMasterEntities()
            : base("name=ProductMasterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<externaluser_regifrom> externaluser_regifrom { get; set; }
        public DbSet<internaluser> internaluser { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<userlogin> userlogin { get; set; }
    }
}