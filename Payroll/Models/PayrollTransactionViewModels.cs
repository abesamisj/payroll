using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Models
{
    public class PayrollTransactionViewModels
    {
        public int PayrollTransactionId { get; set; }

        public int PayrollPeriodId { get; set; }
        public SelectList PayrollPeriods { get; set; }

        [Display(Name = "Selected Payroll Period")]
        public string SelectedPayrollPeriod { get; set; }

        public int UserPersonalInformationId { get; set; }


        public List<PayrollTransactionIncomeViewModels> Incomes { get; set; }


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

    public class PayrollTransactionsPeriodViewModels
    {
        public int PayrollPeriodId { get; set; }
        public SelectList PayrollPeriods { get; set; }
    }
}