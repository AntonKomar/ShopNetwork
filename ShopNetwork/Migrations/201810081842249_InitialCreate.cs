namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.SubGroups",
                c => new
                    {
                        SubGroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Group_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.SubGroupID)
                .ForeignKey("dbo.Groups", t => t.Group_GroupID)
                .Index(t => t.Group_GroupID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProdID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        IsDiscount = c.Boolean(nullable: false),
                        UnityOfMeasurement = c.Int(nullable: false),
                        ImageID_ImageID = c.Int(),
                        SubGroup_SubGroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ProdID)
                .ForeignKey("dbo.Images", t => t.ImageID_ImageID)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupID)
                .Index(t => t.ImageID_ImageID)
                .Index(t => t.SubGroup_SubGroupID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.ImageID);
            
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        MarketID = c.Int(nullable: false, identity: true),
                        City = c.String(maxLength: 20),
                        Street = c.String(maxLength: 20),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MarketID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        LastName = c.String(maxLength: 20),
                        Gender = c.Int(nullable: false),
                        Birth = c.DateTime(nullable: false),
                        DateReg = c.DateTime(nullable: false),
                        City = c.String(maxLength: 20),
                        Phone = c.Int(nullable: false),
                        Email = c.String(maxLength: 30),
                        ImageID_ImageID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Images", t => t.ImageID_ImageID)
                .Index(t => t.ImageID_ImageID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(),
                        ImageID_ImageID = c.Int(),
                    })
                .PrimaryKey(t => t.NewID)
                .ForeignKey("dbo.Images", t => t.ImageID_ImageID)
                .Index(t => t.ImageID_ImageID);
            
            CreateTable(
                "dbo.MarketProducts",
                c => new
                    {
                        Market_MarketID = c.Int(nullable: false),
                        Product_ProdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Market_MarketID, t.Product_ProdID })
                .ForeignKey("dbo.Markets", t => t.Market_MarketID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProdID, cascadeDelete: true)
                .Index(t => t.Market_MarketID)
                .Index(t => t.Product_ProdID);
            
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Product_ProdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Product_ProdID })
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProdID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Product_ProdID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "ImageID_ImageID", "dbo.Images");
            DropForeignKey("dbo.SubGroups", "Group_GroupID", "dbo.Groups");
            DropForeignKey("dbo.Products", "SubGroup_SubGroupID", "dbo.SubGroups");
            DropForeignKey("dbo.UserProducts", "Product_ProdID", "dbo.Products");
            DropForeignKey("dbo.UserProducts", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "ImageID_ImageID", "dbo.Images");
            DropForeignKey("dbo.MarketProducts", "Product_ProdID", "dbo.Products");
            DropForeignKey("dbo.MarketProducts", "Market_MarketID", "dbo.Markets");
            DropForeignKey("dbo.Products", "ImageID_ImageID", "dbo.Images");
            DropIndex("dbo.UserProducts", new[] { "Product_ProdID" });
            DropIndex("dbo.UserProducts", new[] { "User_UserID" });
            DropIndex("dbo.MarketProducts", new[] { "Product_ProdID" });
            DropIndex("dbo.MarketProducts", new[] { "Market_MarketID" });
            DropIndex("dbo.News", new[] { "ImageID_ImageID" });
            DropIndex("dbo.Users", new[] { "ImageID_ImageID" });
            DropIndex("dbo.Products", new[] { "SubGroup_SubGroupID" });
            DropIndex("dbo.Products", new[] { "ImageID_ImageID" });
            DropIndex("dbo.SubGroups", new[] { "Group_GroupID" });
            DropTable("dbo.UserProducts");
            DropTable("dbo.MarketProducts");
            DropTable("dbo.News");
            DropTable("dbo.Users");
            DropTable("dbo.Markets");
            DropTable("dbo.Images");
            DropTable("dbo.Products");
            DropTable("dbo.SubGroups");
            DropTable("dbo.Groups");
        }
    }
}
