namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "KeyPass", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "KeyPass");
        }
    }
}
