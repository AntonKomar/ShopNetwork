namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changetypeofphone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Phone", c => c.String());
            AlterColumn("dbo.People", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Phone", c => c.Int(nullable: false));
            AlterColumn("dbo.Admins", "Phone", c => c.Int(nullable: false));
        }
    }
}
