using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class DeductionViewModels
    {
        public int DeductionId { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Amount can't have more than 2 decimal places")]
        public decimal? DeductionValue { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Deduction Name")]
        public string DeductionName { get; set; }

        [Display(Name = "Deduction Description")]
        public string DeductionDescription { get; set; }

        [Display(Name = "Status")]
        public bool Active { get; set; }
    }
}