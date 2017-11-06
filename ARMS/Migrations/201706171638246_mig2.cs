namespace ARMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicaticants", "InterviewDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Applicaticants", "isSelected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicaticants", "isBackListed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicaticants", "isDateTimeAsssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicaticants", "isDateTimeAsssigned");
            DropColumn("dbo.Applicaticants", "isBackListed");
            DropColumn("dbo.Applicaticants", "isSelected");
            DropColumn("dbo.Applicaticants", "InterviewDateTime");
        }
    }
}
