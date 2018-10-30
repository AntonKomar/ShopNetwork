namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        PriceID = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        ProdID_ProdID = c.Int(),
                    })
                .PrimaryKey(t => t.PriceID)
                .ForeignKey("dbo.Products", t => t.ProdID_ProdID)
                .Index(t => t.ProdID_ProdID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discounts", "ProdID_ProdID", "dbo.Products");
            DropIndex("dbo.Discounts", new[] { "ProdID_ProdID" });
            DropTable("dbo.Discounts");
        }
    }
}
