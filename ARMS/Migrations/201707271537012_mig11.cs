namespace ARMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InterviewMembers", c => c.String());
            AddColumn("dbo.AspNetUsers", "timeshedules", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "timeshedules");
            DropColumn("dbo.AspNetUsers", "InterviewMembers");
        }
    }
}
