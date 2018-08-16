using System;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class PayrollPeriodViewModels
    {   

        public int PayrollPeriodId { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Payroll Period Description")]
        public string PayrollPeriodDescription { get; set; }
        [Display(Name = "Year")]
        public int? Year { get; set; }
        [Display(Name = "Month")]
        public string Month { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payroll Period From")]
        public DateTime PayPeriodFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payroll Period To")]
        public DateTime PayPeriodTo { get; set; }
        [Range(0, 31, ErrorMessage = "Please enter a valid Number")]
        [Display(Name = "Work Days")]
        public int? WorkDays { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Amount can't have more than 2 decimal places")]
        [Display(Name = "Work Hours")]
        public decimal? WorkHours { get; set; }
    }
}