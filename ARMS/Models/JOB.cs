using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ARMS.Models
{
    public class JOB
    {
        [Key]
        public int JOBId { get; set; }
      
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobRequiredQualification { get; set; }
    }
}