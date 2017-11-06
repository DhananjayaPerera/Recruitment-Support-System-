namespace ARMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "emailAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "fullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "fullName");
            DropColumn("dbo.AspNetUsers", "emailAddress");
        }
    }
}
