namespace EagleUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityDocuments",
                c => new
                    {
                        DocumentId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        AssignDate = c.DateTime(nullable: false),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => new { t.DocumentId, t.ActivityId })
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.CourseDocuments",
                c => new
                    {
                        DocumentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        AssignDate = c.DateTime(nullable: false),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => new { t.DocumentId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.ModuleDocuments",
                c => new
                    {
                        DocumentId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        AssignDate = c.DateTime(nullable: false),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => new { t.DocumentId, t.ModuleId })
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.DocumentId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                        AssignDate = c.DateTime(nullable: false),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.CourseId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.DocumentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "OwnerId", c => c.String());
            AddColumn("dbo.Documents", "DocumentTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "RegistrationTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Documents", "DocumentTypeId");
            AddForeignKey("dbo.Documents", "DocumentTypeId", "dbo.DocumentTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "DocumentTypeId", "dbo.DocumentTypes");
            DropForeignKey("dbo.CourseDocuments", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.Assignments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Assignments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ModuleDocuments", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.ModuleDocuments", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.CourseDocuments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ActivityDocuments", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.ActivityDocuments", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Assignments", new[] { "CourseId" });
            DropIndex("dbo.Assignments", new[] { "ApplicationUserId" });
            DropIndex("dbo.ModuleDocuments", new[] { "ModuleId" });
            DropIndex("dbo.ModuleDocuments", new[] { "DocumentId" });
            DropIndex("dbo.CourseDocuments", new[] { "CourseId" });
            DropIndex("dbo.CourseDocuments", new[] { "DocumentId" });
            DropIndex("dbo.Documents", new[] { "DocumentTypeId" });
            DropIndex("dbo.ActivityDocuments", new[] { "ActivityId" });
            DropIndex("dbo.ActivityDocuments", new[] { "DocumentId" });
            DropColumn("dbo.AspNetUsers", "RegistrationTime");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.Documents", "DocumentTypeId");
            DropColumn("dbo.Courses", "OwnerId");
            DropTable("dbo.DocumentTypes");
            DropTable("dbo.Assignments");
            DropTable("dbo.ModuleDocuments");
            DropTable("dbo.CourseDocuments");
            DropTable("dbo.ActivityDocuments");
        }
    }
}
