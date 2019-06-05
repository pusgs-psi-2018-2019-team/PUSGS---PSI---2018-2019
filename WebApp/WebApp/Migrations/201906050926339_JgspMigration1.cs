namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JgspMigration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Timetables", "Line_Id", "dbo.Lines");
            DropIndex("dbo.Timetables", new[] { "Line_Id" });
            RenameColumn(table: "dbo.Timetables", name: "Line_Id", newName: "LineId");
            AlterColumn("dbo.Timetables", "LineId", c => c.Int(nullable: false));
            CreateIndex("dbo.Timetables", "LineId");
            AddForeignKey("dbo.Timetables", "LineId", "dbo.Lines", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timetables", "LineId", "dbo.Lines");
            DropIndex("dbo.Timetables", new[] { "LineId" });
            AlterColumn("dbo.Timetables", "LineId", c => c.Int());
            RenameColumn(table: "dbo.Timetables", name: "LineId", newName: "Line_Id");
            CreateIndex("dbo.Timetables", "Line_Id");
            AddForeignKey("dbo.Timetables", "Line_Id", "dbo.Lines", "Id");
        }
    }
}
