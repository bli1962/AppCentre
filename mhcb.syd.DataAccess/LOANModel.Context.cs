﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mhcb.syd.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LoanDBEntities : DbContext
    {
        public LoanDBEntities()
            : base("name=LoanDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FACILITY_MASTER> FACILITY_MASTER { get; set; }
        public virtual DbSet<LOAN_MASTER> LOAN_MASTER { get; set; }
        public virtual DbSet<LOAN_TRANS> LOAN_TRANS { get; set; }
        public virtual DbSet<EXCH_RATE> EXCH_RATE { get; set; }
        public virtual DbSet<LOAN_MASTER_ACCOUNT_OFFICER> LOAN_MASTER_ACCOUNT_OFFICER { get; set; }
        public virtual DbSet<LOAN_MASTER_BORROWER> LOAN_MASTER_BORROWER { get; set; }
        public virtual DbSet<LOAN_MASTER_RATING> LOAN_MASTER_RATING { get; set; }
        public virtual DbSet<LOAN_MASTER_SECURITY> LOAN_MASTER_SECURITY { get; set; }
        public virtual DbSet<TRANCHE_CONTENT> TRANCHE_CONTENT { get; set; }
        public virtual DbSet<TRANCHE_GUARANTEE> TRANCHE_GUARANTEE { get; set; }
        public virtual DbSet<TRANCHE_MARGIN> TRANCHE_MARGIN { get; set; }
        public virtual DbSet<TRANCHE_MASTER> TRANCHE_MASTER { get; set; }
        public virtual DbSet<TRANCHE_TYPE> TRANCHE_TYPE { get; set; }
        public virtual DbSet<DEPT_PERMISSION> DEPT_PERMISSION { get; set; }
        public virtual DbSet<LCS_CUST_MASTER> LCS_CUST_MASTER { get; set; }
        public virtual DbSet<COVENANT_CONDITION> COVENANT_CONDITION { get; set; }
        public virtual DbSet<COVENANT_CONDITION_GROUP> COVENANT_CONDITION_GROUP { get; set; }
        public virtual DbSet<COVENANT_MASTER> COVENANT_MASTER { get; set; }
        public virtual DbSet<COVENANT_PERMISSION> COVENANT_PERMISSION { get; set; }
        public virtual DbSet<COVENANT_RECORD> COVENANT_RECORD { get; set; }
        public virtual DbSet<COVENANT_TYPE> COVENANT_TYPE { get; set; }
        public virtual DbSet<LOAN_MASTER_COVENANT> LOAN_MASTER_COVENANT { get; set; }
        public virtual DbSet<LOAN_MASTER_STATUS_CODE> LOAN_MASTER_STATUS_CODE { get; set; }
        public virtual DbSet<COVENANT_RECEIPT_TIMING> COVENANT_RECEIPT_TIMING { get; set; }
        public virtual DbSet<COVENANT_FREQ_TYPE> COVENANT_FREQ_TYPE { get; set; }
        public virtual DbSet<PROD_TYPE> PROD_TYPE { get; set; }
        public virtual DbSet<vGUIDE_CUSTOMER_MASTER> vGUIDE_CUSTOMER_MASTER { get; set; }
        public virtual DbSet<STATUS_CODE> STATUS_CODE { get; set; }
    }
}
