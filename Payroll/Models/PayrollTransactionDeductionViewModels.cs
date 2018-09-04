using System;
using System.Collections.Generic;
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
        public decimal CustomDeductionAmount { get; set; }

        public string DefaultDeductionName { get; set; }
    }
}