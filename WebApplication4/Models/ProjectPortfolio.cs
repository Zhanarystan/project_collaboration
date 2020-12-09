using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class ProjectPortfolio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string description { get; set; }
        public ApplicationUser User { get; set; }
    }
}