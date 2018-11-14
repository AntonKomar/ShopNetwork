namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LittleChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Admins", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.People", "Password", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Admins", "KeyPass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "KeyPass", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "Password", c => c.String());
            AlterColumn("dbo.Admins", "Password", c => c.String());
            DropColumn("dbo.Admins", "AdminName");
        }
    }
}
