using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;


namespace ARMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string JobRole { get; set; }
        public string emailAddress { get; set; }
        public string fullName { get; set; }
        public string InterviewMembers { get; set; }
        public string timeshedules { get; set; }

        public List<InterviewShedules> InterviwSheduleList { get; set; }
    }

    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<ARMS.Models.JOB> JOBs { get; set; }

        public System.Data.Entity.DbSet<ARMS.Models.Vacancy> Vacancies { get; set; }

        public System.Data.Entity.DbSet<ARMS.Models.Applicaticant> Applicaticants { get; set; }

       
    }
}