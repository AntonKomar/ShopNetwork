namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPersonAndAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Password", c => c.String());
            AddColumn("dbo.People", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Password");
            DropColumn("dbo.Admins", "Password");
        }
    }
}
