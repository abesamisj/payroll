using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class PayrollTransactionViewModels
    {
        public int PayrollTransactionId { get; set; }
        public int PayrollPeriodId { get; set; }
        public int UserPersonalInformationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? IncomeId { get; set; }
        public decimal? CustomIncomeAmount { get; set; }

        public int? DeductionId { get; set; }
        public decimal? CustomDeductionAmount { get; set; }
        public decimal? TotalDeductionAmount { get; set; }
        public decimal? TotalIncomeAmount { get; set; }

        //payroll period
        public DateTime? PayrollPeriodFrom { get; set; }
        public int? WorkDays { get; set; }
        public decimal? WorkHours { get; set; }

        //employee information
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public decimal? BasicPay { get; set; }

        //department
        public string DepartmentName { get; set; }

        //income
        public string IncomeName { get; set; }
        public decimal? IncomeValue { get; set; }

        //deduction
        public string DeductionName { get; set; }
        public decimal? DeductionValue { get; set; }
    }
}