namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatetoNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Date");
        }
    }
}
