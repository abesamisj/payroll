using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class IncomeViewModels
    {

        public int IncomeId { get; set; }

        //[Required]
        //[Display(Name = "Amount")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        //public decimal? IncomeValue { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Income Name")]
        public string IncomeName { get; set; }

        [Display(Name = "Income Description")]
        public string IncomeDescription { get; set; }

        [Display(Name = "Status")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Order")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Order { get; set; }
    }
}