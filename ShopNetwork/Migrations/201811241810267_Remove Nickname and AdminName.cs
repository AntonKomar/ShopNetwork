namespace ShopNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNicknameandAdminName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Admins", "AdminName");
            DropColumn("dbo.People", "Nickname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Nickname", c => c.String());
            AddColumn("dbo.Admins", "AdminName", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
