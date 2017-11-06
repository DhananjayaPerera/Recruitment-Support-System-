using ARMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ARMS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        static int CurrentAplicantID = 0;
        static string CurrentApplyJOBTitle = "";
        static int CurrentApplyVacancyID = 0;

        //------------------- Load Vacancy Details for View Front End ------------------------------------------//
        public ActionResult Index()
        {
            ViewBag.VacancyList = (from c in db.Vacancies where c.isActivated == true select c).ToList();
            return View();
        }
        //------------------------------------------------------------------------------------------------------//

        //------------------------------------------------------//
        [HttpPost]
        public ActionResult LoadVacancies()
        {
            List<Vacancy> output = (from c in db.Vacancies where c.isActivated == true select c).ToList();
            return Json(output);
        }
        //-----------------------------------------------------//

        //----------------------------- Create New Applicant ----------------------------------------------------//
        [HttpPost]
        public ActionResult SaveApplicationDetails(Applicaticant model)
        {
            string det = "Error";


            List<Vacancy> outputV = (from c in db.Vacancies
                                     where c.VacancyId == model.marks
                                     select c).ToList();
            model.vacancy = outputV[0];
            
            model.marks = 0;
            model.isDateTimeAsssigned = false;
            model.isSelected = false;
            model.isBackListed = false;
            model.isDateTimeAsssigned = false;
            model.InterviewDateTime = DateTime.Now;

            if (ModelState.IsValid)
            {

                db.Applicaticants.Add(model);
                db.SaveChanges();
                det = "Success";
            }
            CurrentAplicantID = model.ApplicantId;
            return Json(det);
        }
        //--------------------------------------------------------------------------------------------------------//


        //---------------- Load Application Submission Form ----------------------//
        public ActionResult ApplicationForm(string id)
        {

            if (id != null)
            {
                int vacancyID = Int32.Parse(id);
                CurrentApplyVacancyID = vacancyID;

                List<Vacancy> output = (from c in db.Vacancies
                                        where c.VacancyId == vacancyID
                                        select c).ToList();
                ViewBag.vacancyJonTitle = output[0].job.JobTitle;
                CurrentApplyJOBTitle = output[0].job.JobTitle;
                ViewBag.VacancyID = vacancyID;
            }

            return View();
        }
        //-----------------------------------------------------------------------//


        //-------------- Save Uploaded Softcoppy of CV --------------------------//
        [HttpPost]
        public ActionResult UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = CurrentAplicantID.ToString() + ".pdf";
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/UploadCV/"), CurrentApplyJOBTitle, CurrentApplyVacancyID.ToString(), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("CV Details Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("CV Details Not uploaded...Error occurred...");
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }  
    }
}