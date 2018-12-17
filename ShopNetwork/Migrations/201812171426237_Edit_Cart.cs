namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Cart : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Carts", name: "Admin_AdminID", newName: "AdminId_AdminID");
            RenameColumn(table: "dbo.Carts", name: "Person_PersonID", newName: "PersonId_PersonID");
            RenameIndex(table: "dbo.Carts", name: "IX_Admin_AdminID", newName: "IX_AdminId_AdminID");
            RenameIndex(table: "dbo.Carts", name: "IX_Person_PersonID", newName: "IX_PersonId_PersonID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Carts", name: "IX_PersonId_PersonID", newName: "IX_Person_PersonID");
            RenameIndex(table: "dbo.Carts", name: "IX_AdminId_AdminID", newName: "IX_Admin_AdminID");
            RenameColumn(table: "dbo.Carts", name: "PersonId_PersonID", newName: "Person_PersonID");
            RenameColumn(table: "dbo.Carts", name: "AdminId_AdminID", newName: "Admin_AdminID");
        }
    }
}
