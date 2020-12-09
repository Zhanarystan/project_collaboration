namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmha1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EnrollmentRequests", "ProjectId", "dbo.Projects");
            AddForeignKey("dbo.EnrollmentRequests", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnrollmentRequests", "ProjectId", "dbo.Projects");
            AddForeignKey("dbo.EnrollmentRequests", "ProjectId", "dbo.Projects", "Id");
        }
    }
}
