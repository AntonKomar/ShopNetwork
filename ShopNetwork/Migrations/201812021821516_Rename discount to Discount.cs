namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamediscounttoDiscount : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "discount_PriceID" });
            CreateIndex("dbo.Products", "Discount_PriceID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "Discount_PriceID" });
            CreateIndex("dbo.Products", "discount_PriceID");
        }
    }
}
