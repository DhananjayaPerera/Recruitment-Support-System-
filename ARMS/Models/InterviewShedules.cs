using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS.Models
{
    public class InterviewShedules
    {
        public int Id { get; set; }
        public DateTime sheduleDateTime { get; set;}
        public int timePeriod { get; set; }
        public int ApplicantID { get; set; }
    }
}