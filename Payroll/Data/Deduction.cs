//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Deduction
    {
        public int DeductionId { get; set; }
        public Nullable<decimal> DeductionValue { get; set; }
        public string DeductionName { get; set; }
        public string DeductionDescription { get; set; }
        public Nullable<int> Active { get; set; }
    }
}
