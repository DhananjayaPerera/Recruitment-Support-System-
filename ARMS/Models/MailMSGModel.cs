using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMS.Models
{
    public class MailMSGModel
    {       
        public string From {get;set;}
        public string To { get; set; }
        public string MailServer { get; set; }
        public int MailServerPort { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string CredentialUserName { get; set; }
        public string CredentialPassword { get; set; }
    }
}