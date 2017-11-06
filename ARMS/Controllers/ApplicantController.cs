using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARMS.Models;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;

namespace ARMS.Controllers
{
    public class ApplicantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.VacancyList = (from c in db.Vacancies where c.isActivated == true select c).ToList();
            return View();
        }


        public ActionResult addMarks()
        {
            if (User.Identity.Name != "Admin") {
                ApplicationUser applicattionUserModel = (from c in db.Users
                                                         where c.UserName == User.Identity.Name
                                                         select c).ToList()[0];
                List<string> ApplicantIdList = applicattionUserModel.InterviewMembers.Split(',').ToList();
                List<Applicaticant> AllApplicants = new List<Applicaticant>();

                for (int i = 0; i < ApplicantIdList.Count - 1; i++)
                {
                    int id = Int32.Parse(ApplicantIdList[i]);
                    Applicaticant x = db.Applicaticants.Find(id);
                    AllApplicants.Add(x);
                }
                ViewBag.AssignApplications = AllApplicants;
            }
           
            return View();
        }


        public ActionResult shortList()
        {
            
              
                List<Applicaticant> AllApplicants = (from c in db.Applicaticants
                                                     orderby c.marks descending
                                                     select c).ToList();

                ViewBag.AllApplications = AllApplicants;
            

            return View();
        }
        

        //----------------------------- Load Applicant Details ----------------------------------------------//
        [HttpPost]
        public ActionResult SaveMarks(Applicaticant model)
        {
            string result = "ok";
            try
            {
                Applicaticant applicatModel = db.Applicaticants.Find(model.ApplicantId);
                applicatModel.marks = model.marks;
                if (ModelState.IsValid)
                {
                    db.Entry(applicatModel).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = "not";
            }
            return Json(result);
        }
        //-------------------------------------------------------------------------------------------------//


        //----------------------------- Load Applicant Details ----------------------------------------------//
        [HttpPost]
        public ActionResult LoadApplicantDetails(Vacancy model)
        {
            List<Applicaticant> output = (from c in db.Applicaticants
                                          where c.VacancyId == model.VacancyId
                                          select c).ToList();
           

            
            return Json(output);
        }
        //-------------------------------------------------------------------------------------------------//



        //--------------------- Load Applicant Details in new Tab -----------------------------------------//
        public ActionResult aplicantdetails(int id)
        {
            ViewBag.ApplicantDetails = (from c in db.Applicaticants
                                          where c.ApplicantId == id
                                          select c).ToList()[0];
            ViewBag.Age = DateTime.Now.Year - ViewBag.ApplicantDetails.DOB.Year;

            return View();
        }
        //-------------------------------------------------------------------------------------------------//

        //--------------------- Load ApplicantEdit Details in new Tab -----------------------------------------//
        public ActionResult aplicanteditdetails(int id)
        {
            ViewBag.ApplicantEditDetails = (from c in db.Applicaticants
                                        where c.ApplicantId == id && c.isAssignedTimeShedule == true
                                        select c).ToList()[0];
            ViewBag.Age = DateTime.Now.Year - ViewBag.ApplicantEditDetails.DOB.Year;
            ViewBag.DurationTime = ViewBag.ApplicantEditDetails.TimeDuration + "min";

            return View();
        }
        //-------------------------------------------------------------------------------------------------//




        //------------------------- Send Emails -----------------------------------------------------------//
        [HttpPost]
        public ActionResult SendMailToApplicant(MailMSGModel model)
        {

            try
            {
                model.CredentialUserName = "tha2993desh@gmail.com";
                model.CredentialPassword = "";
                model.From = "tha2993desh@gmail.com";
                model.To = "tharuka1993deshan@gmail.com";
                model.MailServer = "smtp.gmail.com";
                model.MailServerPort = 587;

                string jobTitle = model.EmailSubject;
                model.EmailSubject = "To inform Job Interview For " + jobTitle;

                string Interviewshedule = model.EmailBody;
                model.EmailBody = "We would like to inform that you shortlisted for interview. Interview will be held on " + Interviewshedule;

                MailMessage mailMSG = new MailMessage(model.From, model.To);
                mailMSG.Subject = model.EmailSubject;
                mailMSG.Body = model.EmailBody;

                SmtpClient smtpClient = new SmtpClient(model.MailServer, model.MailServerPort);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = model.CredentialUserName,
                    Password = model.CredentialPassword
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMSG);
            }
            catch (Exception ex) { 
            
            }           
            return Json("success");
        }
        //---------------------------------------------------------------------------------------------------------//



        //---------------- Open CV softcopy PDF in new Tab -------------------------------------------------------//
        public ActionResult OpenPDF(string path, string fileName)
        {

            string pathString = Path.Combine(Server.MapPath(path), fileName);
            FileStream fs = null;
            FileStreamResult fsr = null;

            try
            {
                fs = new FileStream(pathString,
                                            FileMode.Open,
                                            FileAccess.Read
                                        );               
            }

            catch (Exception ex) {
                pathString = Path.Combine(Server.MapPath("~/UploadCV/"), "notFound.pdf");
                fs = new FileStream(pathString,
                                            FileMode.Open,
                                            FileAccess.Read
                                        );             
            }
            fsr = new FileStreamResult(fs, "application/pdf");
            return fsr;
        }
        //---------------------------------------------------------------------------------------------------------//


        
        //----------------------------- Load Applicant Details ----------------------------------------------//
        [HttpPost]
        public ActionResult LoadInterviewPanelMembers(Applicaticant model)
        {
            List<ApplicationUser> output = (from c in db.Users
                                          where c.UserName != "Admin"
                                          select c).ToList();
                     
            return Json(output);
        }
        //-------------------------------------------------------------------------------------------------//


        //----------------- Send emails and Applicant Assign date time details ------------------------------//
        [HttpPost]
        public ActionResult SendEmailsAndSaveDetails(Applicaticant model)
        {
            Applicaticant applicatModel = db.Applicaticants.Find(model.ApplicantId);

            applicatModel.InterviewDateTime = model.InterviewDateTime;
            List<string>  members= model.City.Split(',').ToList();
            applicatModel.AssignInterviewMemberNames = model.City;

            model.email= Regex.Replace(model.email, @"\s+", "");

            applicatModel.isAssignedTimeShedule = true;
            applicatModel.TimeDuration=Int32.Parse(model.ZipCode);

            if (ModelState.IsValid)
            {
                db.Entry(applicatModel).State = EntityState.Modified;
                db.SaveChanges();
            }


          
            string output = "";
            bool isSendToMember=true;
            bool isSendToApplicant = false;
            try
            {
                for (int i = 0; i < members.Count - 1; i++)
                {
                    string memName = members[i];          
                    ApplicationUser applicattionUserModel = (from c in db.Users
                        where c.UserName == memName
                          select c).ToList()[0];
                    applicattionUserModel.InterviewMembers += applicatModel.ApplicantId + ",";
                    applicattionUserModel.timeshedules += applicatModel.InterviewDateTime.ToString() + ",";
                    if (ModelState.IsValid)
                    {
                        db.Entry(applicattionUserModel).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    string PanelMemberEmail = returnSystemUserEmailAddress(members[i]);
                    if (SendMailToInterViewPanel(PanelMemberEmail, model.Gender, model.InterviewDateTime.ToString()) == false) {
                        isSendToMember = false;
                    }                  
                }

                if (SendMailToJOBApplicant(model.email, model.Gender,model.InterviewDateTime.ToString()))
                {
                    isSendToApplicant=true;
                }

                if (isSendToApplicant==true && isSendToMember == true)
                {
                    output = "AllSuccess";
                }
                if (isSendToApplicant==true && isSendToMember == false)
                {
                    output = "ApplicantSuccess";
                }
                if (isSendToApplicant==false && isSendToMember == true)
                {
                    output = "MemberSuccess";
                }

            }
            catch (Exception ex)
            {
                output = ex.ToString();
            }        
            return Json(output);
        }
        //-------------------------------------------------------------------------------------------------//


        public string returnSystemUserEmailAddress (string userName){
            List<ApplicationUser> output = (from c in db.Users
                                            where c.UserName == userName
                                            select c).ToList();
            return output[0].emailAddress;
        }



        //--------------------- Send E-Mail to Applicant --------------------------------------------------//

        public bool SendMailToJOBApplicant(string ApplicantEmail, string jobTitle,string InterviewShedule)
        {
            bool det = false;
            try
            {
                string CredentialUserName = "armssyetm1993@gmail.com";
                string CredentialPassword = "arms@123";
                string From = "armssyetm1993@gmail.com";
                string To = ApplicantEmail;
                string MailServer = "smtp.gmail.com";
                int MailServerPort = 587;

               
                string EmailSubject = "To inform Job Interview For " + jobTitle;
              
                string EmailBody = "We would like to inform that you shortlisted for interview. Interview will be held on " + InterviewShedule;

                MailMessage mailMSG = new MailMessage(From, To);
                mailMSG.Subject = EmailSubject;
                mailMSG.Body = EmailBody;

                SmtpClient smtpClient = new SmtpClient(MailServer, MailServerPort);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = CredentialUserName,
                    Password = CredentialPassword
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMSG);
                det = true;
            }
            catch (Exception ex)
            {

            }
            return det;
        }



        //--------------------- Send E-Mail to Applicant --------------------------------------------------//

        public bool SendMailToInterViewPanel(string PanelMemnerEmail, string jobTitle, string InterviewShedule)
        {
            bool det = false;
            try
            {
                string CredentialUserName = "armssyetm1993@gmail.com";
                string CredentialPassword = "arms@123";
                string From = "armssyetm1993@gmail.com";
                string To = PanelMemnerEmail;
                string MailServer = "smtp.gmail.com";
                int MailServerPort = 587;


                string EmailSubject = "To inform Job Interview For " + jobTitle;

                string EmailBody = "You are assigned for job interview. Interview will be held on " + InterviewShedule;

                MailMessage mailMSG = new MailMessage(From, To);
                mailMSG.Subject = EmailSubject;
                mailMSG.Body = EmailBody;

                SmtpClient smtpClient = new SmtpClient(MailServer, MailServerPort);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = CredentialUserName,
                    Password = CredentialPassword
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMSG);
                det = true;
            }
            catch (Exception ex)
            {

            }
            return det;
        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}
