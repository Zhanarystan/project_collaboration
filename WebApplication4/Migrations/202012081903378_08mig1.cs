namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08mig1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EnrollmentRequests", "RequestMessage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EnrollmentRequests", "RequestMessage", c => c.String());
        }
    }
}
