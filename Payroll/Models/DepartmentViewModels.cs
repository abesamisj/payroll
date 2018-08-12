using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class DepartmentViewModels
    {
        public int DepartmentId { get; set; }


        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "Department Description")]
        public string DepartmentDescription { get; set; }

        [Display(Name = "Status")]
        public bool Active { get; set; }
    }
}