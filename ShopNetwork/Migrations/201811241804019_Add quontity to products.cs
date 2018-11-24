namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addquontitytoproducts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ImageID_ImageID", "dbo.Images");
            DropForeignKey("dbo.Admins", "ImageID_ImageID", "dbo.Images");
            DropForeignKey("dbo.News", "ImageID_ImageID", "dbo.Images");
            DropForeignKey("dbo.People", "ImageID_ImageID", "dbo.Images");
            DropIndex("dbo.Admins", new[] { "ImageID_ImageID" });
            DropIndex("dbo.Products", new[] { "ImageID_ImageID" });
            DropIndex("dbo.News", new[] { "ImageID_ImageID" });
            DropIndex("dbo.People", new[] { "ImageID_ImageID" });
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureID = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.PictureID);
            
            AddColumn("dbo.Admins", "PictureID_PictureID", c => c.Int());
            AddColumn("dbo.Products", "Quontity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "PictureID_PictureID", c => c.Int());
            AddColumn("dbo.News", "PictureID_PictureID", c => c.Int());
            AddColumn("dbo.People", "PictureID_PictureID", c => c.Int());
            CreateIndex("dbo.Admins", "PictureID_PictureID");
            CreateIndex("dbo.Products", "PictureID_PictureID");
            CreateIndex("dbo.News", "PictureID_PictureID");
            CreateIndex("dbo.People", "PictureID_PictureID");
            AddForeignKey("dbo.Products", "PictureID_PictureID", "dbo.Pictures", "PictureID");
            AddForeignKey("dbo.Admins", "PictureID_PictureID", "dbo.Pictures", "PictureID");
            AddForeignKey("dbo.News", "PictureID_PictureID", "dbo.Pictures", "PictureID");
            AddForeignKey("dbo.People", "PictureID_PictureID", "dbo.Pictures", "PictureID");
            DropColumn("dbo.Admins", "ImageID_ImageID");
            DropColumn("dbo.Products", "ImageID_ImageID");
            DropColumn("dbo.News", "ImageID_ImageID");
            DropColumn("dbo.People", "ImageID_ImageID");
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.ImageID);
            
            AddColumn("dbo.People", "ImageID_ImageID", c => c.Int());
            AddColumn("dbo.News", "ImageID_ImageID", c => c.Int());
            AddColumn("dbo.Products", "ImageID_ImageID", c => c.Int());
            AddColumn("dbo.Admins", "ImageID_ImageID", c => c.Int());
            DropForeignKey("dbo.People", "PictureID_PictureID", "dbo.Pictures");
            DropForeignKey("dbo.News", "PictureID_PictureID", "dbo.Pictures");
            DropForeignKey("dbo.Admins", "PictureID_PictureID", "dbo.Pictures");
            DropForeignKey("dbo.Products", "PictureID_PictureID", "dbo.Pictures");
            DropIndex("dbo.People", new[] { "PictureID_PictureID" });
            DropIndex("dbo.News", new[] { "PictureID_PictureID" });
            DropIndex("dbo.Products", new[] { "PictureID_PictureID" });
            DropIndex("dbo.Admins", new[] { "PictureID_PictureID" });
            DropColumn("dbo.People", "PictureID_PictureID");
            DropColumn("dbo.News", "PictureID_PictureID");
            DropColumn("dbo.Products", "PictureID_PictureID");
            DropColumn("dbo.Products", "Quontity");
            DropColumn("dbo.Admins", "PictureID_PictureID");
            DropTable("dbo.Pictures");
            CreateIndex("dbo.People", "ImageID_ImageID");
            CreateIndex("dbo.News", "ImageID_ImageID");
            CreateIndex("dbo.Products", "ImageID_ImageID");
            CreateIndex("dbo.Admins", "ImageID_ImageID");
            AddForeignKey("dbo.People", "ImageID_ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.News", "ImageID_ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.Admins", "ImageID_ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.Products", "ImageID_ImageID", "dbo.Images", "ImageID");
        }
    }
}
