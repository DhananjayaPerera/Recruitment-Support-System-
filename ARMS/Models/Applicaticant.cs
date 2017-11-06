using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARMS.Models
{
    public class Applicaticant
    {
        [Key]
        public int ApplicantId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }

        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public int marks { get; set; }
        public DateTime InterviewDateTime { get; set; }
        public bool isSelected { get; set; }
        public bool isBackListed { get; set; }
        public bool isDateTimeAsssigned { get; set; }
        public bool isAssignedTimeShedule { get; set; }

        public string AssignInterviewMemberNames { get; set; }
        public int TimeDuration { get; set; }
        
        public int VacancyId { get; set; }
        public List<string> AssignInterviewMembers { get; set; }



        [ForeignKey("VacancyId")]
        public virtual Vacancy vacancy { get; set; }
    }
}