using System;
using System.Collections.Generic;
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
        public decimal CustomIncomeAmount { get; set; }

        public string DefaultIncomeName { get; set; }


    }
}