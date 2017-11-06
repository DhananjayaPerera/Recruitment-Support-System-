namespace ARMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicaticants", "AssignInterviewMemberNames", c => c.String());
            AddColumn("dbo.Applicaticants", "TimeDuration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicaticants", "TimeDuration");
            DropColumn("dbo.Applicaticants", "AssignInterviewMemberNames");
        }
    }
}
