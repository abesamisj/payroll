using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class PayrollViewModels
    {
        public int PayrollId { get; set; }
        public int UserPersonalInformationId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Month { get; set; }
        public int PayPeriodFrom { get; set; }
        public int PayPeriodTo { get; set; }
        public int WorkDays { get; set; }
        public decimal WorkHours { get; set; }
        public int IncomeId { get; set; }
        public decimal CustomIncomeAmount { get; set; }
        public int DeductionId { get; set; }
        public decimal CustomDeductionAmount { get; set; }
    }
}