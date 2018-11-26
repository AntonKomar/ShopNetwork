namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editdiscount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discounts", "ProdID_ProdID", "dbo.Products");
            DropIndex("dbo.Discounts", new[] { "ProdID_ProdID" });
            AddColumn("dbo.Products", "discount_PriceID", c => c.Int());
            CreateIndex("dbo.Products", "discount_PriceID");
            AddForeignKey("dbo.Products", "discount_PriceID", "dbo.Discounts", "PriceID");
            DropColumn("dbo.Products", "IsDiscount");
            DropColumn("dbo.Discounts", "ProdID_ProdID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discounts", "ProdID_ProdID", c => c.Int());
            AddColumn("dbo.Products", "IsDiscount", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Products", "discount_PriceID", "dbo.Discounts");
            DropIndex("dbo.Products", new[] { "discount_PriceID" });
            DropColumn("dbo.Products", "discount_PriceID");
            CreateIndex("dbo.Discounts", "ProdID_ProdID");
            AddForeignKey("dbo.Discounts", "ProdID_ProdID", "dbo.Products", "ProdID");
        }
    }
}
