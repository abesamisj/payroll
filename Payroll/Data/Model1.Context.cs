﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Payroll.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PayrollEntities : DbContext
    {
        public PayrollEntities()
            : base("name=PayrollEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Deduction> Deductions { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<UserPersonalInformation> UserPersonalInformations { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PayrollTransaction> PayrollTransactions { get; set; }
        public virtual DbSet<PayrollPeriod> PayrollPeriods { get; set; }
    }
}
