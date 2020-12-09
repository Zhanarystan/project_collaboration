namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnrollmentRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestMessage = c.String(),
                        UserId = c.Int(),
                        ProjectId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProjectId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                        SpecificationId = c.Int(),
                        PostedById = c.Int(),
                        PostedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PostedBy_Id)
                .ForeignKey("dbo.Specifications", t => t.SpecificationId)
                .Index(t => t.SpecificationId)
                .Index(t => t.PostedBy_Id);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        ProjectId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProjectId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ProjectPortfolios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        description = c.String(),
                        UserId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.AspNetUsers", "SpecificationId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "SpecificationId");
            AddForeignKey("dbo.AspNetUsers", "SpecificationId", "dbo.Specifications", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectPortfolios", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EnrollmentRequests", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EnrollmentRequests", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Projects", "PostedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "SpecificationId", "dbo.Specifications");
            DropIndex("dbo.ProjectPortfolios", new[] { "User_Id" });
            DropIndex("dbo.Enrollments", new[] { "User_Id" });
            DropIndex("dbo.Enrollments", new[] { "ProjectId" });
            DropIndex("dbo.AspNetUsers", new[] { "SpecificationId" });
            DropIndex("dbo.Projects", new[] { "PostedBy_Id" });
            DropIndex("dbo.Projects", new[] { "SpecificationId" });
            DropIndex("dbo.EnrollmentRequests", new[] { "User_Id" });
            DropIndex("dbo.EnrollmentRequests", new[] { "ProjectId" });
            DropColumn("dbo.AspNetUsers", "SpecificationId");
            DropTable("dbo.ProjectPortfolios");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Specifications");
            DropTable("dbo.Projects");
            DropTable("dbo.EnrollmentRequests");
        }
    }
}
