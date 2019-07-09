namespace ArchivoUH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administratives", "Serial1", c => c.String(maxLength: 3));
            AlterColumn("dbo.Administratives", "Serial2", c => c.String(maxLength: 3));
            AlterColumn("dbo.Graduateds", "Serial1", c => c.String(maxLength: 3));
            AlterColumn("dbo.Graduateds", "Serial2", c => c.String(maxLength: 3));
            AlterColumn("dbo.Leaveds", "Serial1", c => c.String(maxLength: 3));
            AlterColumn("dbo.Leaveds", "Serial2", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leaveds", "Serial2", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Leaveds", "Serial1", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Graduateds", "Serial2", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Graduateds", "Serial1", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Administratives", "Serial2", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Administratives", "Serial1", c => c.String(nullable: false, maxLength: 3));
        }
    }
}
