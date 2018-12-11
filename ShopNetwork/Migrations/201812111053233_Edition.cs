namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edition : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Name", c => c.String(maxLength: 20));
        }
    }
}
