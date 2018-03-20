namespace EagleUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Content", c => c.Binary());
            AddColumn("dbo.Documents", "FileType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "FileType");
            DropColumn("dbo.Documents", "Content");
        }
    }
}
