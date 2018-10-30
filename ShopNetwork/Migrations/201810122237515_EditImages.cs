namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "FilePath", c => c.String());
            DropColumn("dbo.Images", "ImageData");
            DropColumn("dbo.Images", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImageMimeType", c => c.String());
            AddColumn("dbo.Images", "ImageData", c => c.Binary());
            DropColumn("dbo.Images", "FilePath");
        }
    }
}
