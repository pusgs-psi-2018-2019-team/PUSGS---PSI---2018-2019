namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JgspMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timetables", "Times", c => c.String());
            AlterColumn("dbo.Pricelists", "From", c => c.String());
            AlterColumn("dbo.Pricelists", "To", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pricelists", "To", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pricelists", "From", c => c.DateTime(nullable: false));
            DropColumn("dbo.Timetables", "Times");
        }
    }
}
