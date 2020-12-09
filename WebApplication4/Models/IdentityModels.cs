using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication4.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? SpecificationId { get; set; }
        public Specifications Specification { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
        public virtual ICollection<Enrollments> Enrollments { get; set; }
        public virtual ICollection<ProjectPortfolio> ProjectPortfolios { get; set; }
        public virtual ICollection<EnrollmentRequests> EnrollmentRequests { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        {
        }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Specifications> Specifications { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
        public DbSet<ProjectPortfolio> ProjectPortfolios { get; set; }
        public DbSet<EnrollmentRequests> EnrollmentRequests { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Projects).WithOptional(a => a.PostedBy).HasForeignKey(a => a.PostedById);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.EnrollmentRequests).WithOptional(a => a.User).HasForeignKey(a => a.UserId);
            modelBuilder.Entity<EnrollmentRequests>().HasOptional(c => c.Project).WithMany(c => c.EnrollmentRequests).HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<Projects>().HasMany(c => c.EnrollmentRequests).WithOptional(a => a.Project).HasForeignKey(a => a.ProjectId);

            
           

            base.OnModelCreating(modelBuilder);

        }

    }
}