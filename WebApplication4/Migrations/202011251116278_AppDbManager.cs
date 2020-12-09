namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppDbManager : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EnrollmentRequests", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Enrollments", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.EnrollmentRequests", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Enrollments", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Enrollments", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.EnrollmentRequests", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Enrollments", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.EnrollmentRequests", name: "UserId", newName: "User_Id");
        }
    }
}
