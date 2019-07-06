namespace ArchivoUH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_ubications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Graduateds", "ProvinceId", c => c.Int(nullable: true));
            AddColumn("dbo.Graduateds", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Graduateds", "ProvinceId");
            CreateIndex("dbo.Graduateds", "CountryId");
            AddForeignKey("dbo.Graduateds", "CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.Graduateds", "ProvinceId", "dbo.Provinces", "ProvinceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Graduateds", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Graduateds", "CountryId", "dbo.Countries");
            DropIndex("dbo.Graduateds", new[] { "CountryId" });
            DropIndex("dbo.Graduateds", new[] { "ProvinceId" });
            DropColumn("dbo.Graduateds", "CountryId");
            DropColumn("dbo.Graduateds", "ProvinceId");
        }
    }
}
