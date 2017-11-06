using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMS.Models
{
    public class Vacancy
    {
        [Key]
        public int VacancyId { get; set; }

        public DateTime closingDate { get; set; }
        public DateTime publishedDate { get; set; }
        public string publishedBy { get; set; }
        public bool isActivated { get; set; }
       
        public int JOBId { get; set; }

        [ForeignKey("JOBId")]
        public virtual JOB job { get; set; }

      
    }
}