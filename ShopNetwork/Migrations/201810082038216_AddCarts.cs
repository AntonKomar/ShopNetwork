namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProducts", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.UserProducts", "Product_ProdID", "dbo.Products");
            DropIndex("dbo.UserProducts", new[] { "User_UserID" });
            DropIndex("dbo.UserProducts", new[] { "Product_ProdID" });
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        Product_ProdID = c.Int(nullable: false),
                        Cart_CartID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProdID, t.Cart_CartID })
                .ForeignKey("dbo.Products", t => t.Product_ProdID, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_CartID, cascadeDelete: true)
                .Index(t => t.Product_ProdID)
                .Index(t => t.Cart_CartID);
            
            DropTable("dbo.UserProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Product_ProdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Product_ProdID });
            
            DropForeignKey("dbo.Carts", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.ProductCarts", "Cart_CartID", "dbo.Carts");
            DropForeignKey("dbo.ProductCarts", "Product_ProdID", "dbo.Products");
            DropIndex("dbo.ProductCarts", new[] { "Cart_CartID" });
            DropIndex("dbo.ProductCarts", new[] { "Product_ProdID" });
            DropIndex("dbo.Carts", new[] { "User_UserID" });
            DropTable("dbo.ProductCarts");
            DropTable("dbo.Carts");
            CreateIndex("dbo.UserProducts", "Product_ProdID");
            CreateIndex("dbo.UserProducts", "User_UserID");
            AddForeignKey("dbo.UserProducts", "Product_ProdID", "dbo.Products", "ProdID", cascadeDelete: true);
            AddForeignKey("dbo.UserProducts", "User_UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
