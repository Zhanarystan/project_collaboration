using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Models
{
    public class EnrollmentRequests
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Remote("CheckMessage", "Home", ErrorMessage = "Message is not valid.")]
        public string RequestMessage { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? ProjectId { get; set; }
        public virtual Projects Project { get; set; }

    }
}