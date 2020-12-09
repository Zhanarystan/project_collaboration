using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Models
{
    public class Projects
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Field must be set")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Field must be set")]
        [Display(Name ="Description")]
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }

        public int? SpecificationId { get; set; }
        public Specifications Specification { get; set; }
        
        //Reference to user.id
        public string? PostedById { get; set; }
        public ApplicationUser PostedBy { get; set; }
        public virtual ICollection<Enrollments> Enrollments { get; set; }
        public virtual ICollection<EnrollmentRequests> EnrollmentRequests { get; set; }

    }
}