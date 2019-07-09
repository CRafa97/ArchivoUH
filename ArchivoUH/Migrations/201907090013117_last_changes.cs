namespace ArchivoUH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last_changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Graduateds", "FacultyTome", c => c.Int(nullable: false));
            AddColumn("dbo.Graduateds", "FacultyFolio", c => c.Int(nullable: false));
            AddColumn("dbo.Graduateds", "FacultyNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.KeyWords", "Name", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Faculties", "FacultyTome");
            DropColumn("dbo.Faculties", "FacultyFolio");
            DropColumn("dbo.Faculties", "FacultyNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faculties", "FacultyNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "FacultyFolio", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "FacultyTome", c => c.Int(nullable: false));
            AlterColumn("dbo.KeyWords", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Graduateds", "FacultyNumber");
            DropColumn("dbo.Graduateds", "FacultyFolio");
            DropColumn("dbo.Graduateds", "FacultyTome");
        }
    }
}
