using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Models
{
    public class PayrollTransactionDeductionViewModels
    {
        [HiddenInput(DisplayValue = false)]
        public int PayrollTransactionDeductionId { get; set; }
        public int UserPersonalInformationid { get; set; }
        public int PayrollPeriodId { get; set; }
        public int DeductionId { get; set; }
        [Display(Name = "Amount")]
        public decimal CustomDeductionAmount { get; set; }
        [Display(Name = "Deduction")]
        public string DefaultDeductionName { get; set; }
    }
}