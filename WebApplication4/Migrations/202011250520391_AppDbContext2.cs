namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppDbContext2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EnrollmentRequests", "UserId");
            DropColumn("dbo.Projects", "PostedById");
            DropColumn("dbo.Enrollments", "UserId");
            DropColumn("dbo.ProjectPortfolios", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectPortfolios", "UserId", c => c.Int());
            AddColumn("dbo.Enrollments", "UserId", c => c.Int());
            AddColumn("dbo.Projects", "PostedById", c => c.Int());
            AddColumn("dbo.EnrollmentRequests", "UserId", c => c.Int());
        }
    }
}
