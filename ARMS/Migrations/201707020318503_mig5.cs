namespace ARMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterviewShedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        sheduleDateTime = c.DateTime(nullable: false),
                        timePeriod = c.Int(nullable: false),
                        ApplicantID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InterviewShedules", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InterviewShedules", new[] { "ApplicationUser_Id" });
            DropTable("dbo.InterviewShedules");
        }
    }
}
