using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Models
{
    public class EmployeeListModel
    {
        public int UserPersonalInformationID { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Basic Pay")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Amount can't have more than 2 decimal places")]
        public decimal? BasicPay { get; set; }

        [Display(Name = "Status")]
        public bool Active { get; set; }

    }

    public class AssignDepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public SelectList Departments { get; set; }

        [Display(Name = "Selected Department")]
        public string SelectedDepartment { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int UserPersonalInformationId { get; set; }

    }

    public class AddIncomeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserPersonalInformationId { get; set; }
        public string Name { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int IncomeId { get; set; }
        public SelectList Incomes { get; set; }

        [Display(Name = "Selected Income")]
        public string SelectedIncomeName { get; set; }
        [Display(Name = "Default Amount")]
        public decimal? SelectedIncomeAmount { get; set; }
        [Display(Name = "Custom Amount")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Amount can't have more than 2 decimal places")]
        public decimal SelectedCustomAmount { get; set; }

    }

    public class AvailableIncomesViewModel
    {
        public int IncomeId { get; set; }
        public decimal? IncomeAmount { get; set; }
        public string IncomeName { get; set; }
    }

    public class AssignedEmployeeIncomesViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserPersonalInformationId { get; set; }
        public string Name { get; set; }

        public int AssignedEmployeeIncomeId { get; set; }
        public int IncomeId { get; set; }
        public decimal? IncomeAmount { get; set; }
        public decimal? CustomIncomeAmount { get; set; }
        public string IncomeName { get; set; }
    }

    public class AssignedEmployeeDeductionsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserPersonalInformationId { get; set; }
        public string Name { get; set; }

        public int AssignedEmployeeDeductionsId { get; set; }
        public int DeductionId { get; set; }
        public decimal? DeductionAmount { get; set; }
        public decimal? CustomIncomeAmount { get; set; }
        public string DeductionName { get; set; }
    }

    public class AddDeductionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserPersonalInformationId { get; set; }
        public string Name { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int DeductionId { get; set; }
        public SelectList Deductions { get; set; }

        [Display(Name = "Selected Deduction")]
        public string SelectedDeductionName { get; set; }
        [Display(Name = "Default Amount")]
        public decimal? SelectedDeductionAmount { get; set; }
        [Display(Name = "Custom Amount")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Amount can't have more than 2 decimal places")]
        public decimal? SelectedCustomAmount { get; set; }

    }
}