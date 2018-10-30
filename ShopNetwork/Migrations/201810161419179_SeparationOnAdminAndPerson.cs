namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeparationOnAdminAndPerson : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "ImageID_ImageID", "dbo.Images");
            DropIndex("dbo.Carts", new[] { "User_UserID" });
            DropIndex("dbo.Users", new[] { "ImageID_ImageID" });
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.AdminID)
                .ForeignKey("dbo.Images", t => t.ImageID_ImageID)
                .Index(t => t.ImageID_ImageID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        Nickname = c.String(),
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
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.Images", t => t.ImageID_ImageID)
                .Index(t => t.ImageID_ImageID);
            
            AddColumn("dbo.Carts", "Admin_AdminID", c => c.Int());
            AddColumn("dbo.Carts", "Person_PersonID", c => c.Int());
            CreateIndex("dbo.Carts", "Admin_AdminID");
            CreateIndex("dbo.Carts", "Person_PersonID");
            AddForeignKey("dbo.Carts", "Admin_AdminID", "dbo.Admins", "AdminID");
            AddForeignKey("dbo.Carts", "Person_PersonID", "dbo.People", "PersonID");
            DropColumn("dbo.Carts", "User_UserID");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Carts", "User_UserID", c => c.Int());
            DropForeignKey("dbo.People", "ImageID_ImageID", "dbo.Images");
            DropForeignKey("dbo.Carts", "Person_PersonID", "dbo.People");
            DropForeignKey("dbo.Admins", "ImageID_ImageID", "dbo.Images");
            DropForeignKey("dbo.Carts", "Admin_AdminID", "dbo.Admins");
            DropIndex("dbo.People", new[] { "ImageID_ImageID" });
            DropIndex("dbo.Carts", new[] { "Person_PersonID" });
            DropIndex("dbo.Carts", new[] { "Admin_AdminID" });
            DropIndex("dbo.Admins", new[] { "ImageID_ImageID" });
            DropColumn("dbo.Carts", "Person_PersonID");
            DropColumn("dbo.Carts", "Admin_AdminID");
            DropTable("dbo.People");
            DropTable("dbo.Admins");
            CreateIndex("dbo.Users", "ImageID_ImageID");
            CreateIndex("dbo.Carts", "User_UserID");
            AddForeignKey("dbo.Users", "ImageID_ImageID", "dbo.Images", "ImageID");
            AddForeignKey("dbo.Carts", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
