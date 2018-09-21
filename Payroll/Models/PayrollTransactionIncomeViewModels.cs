using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Models
{
    public class PayrollTransactionIncomeViewModels
    {
        [HiddenInput(DisplayValue = false)]
        public int PayrollTransactionIncomeId { get; set; }
        public int UserPersonalInformationid { get; set; }
        public int PayrollPeriodId { get; set; }
        public int IncomeId { get; set; }
        [Display(Name = "Amount")]
        public decimal CustomIncomeAmount { get; set; }
        [Display(Name = "Income")]
        public string DefaultIncomeName { get; set; }


    }
}