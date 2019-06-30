namespace ArchivoUH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class administratives : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administratives", "AdministrativeName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Administratives", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Administratives", "Description", c => c.String());
            AlterColumn("dbo.Administratives", "AdministrativeName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
