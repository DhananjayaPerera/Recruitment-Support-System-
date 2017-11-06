using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARMS.Models;
using System.IO;
using CenUtility;

namespace ARMS.Controllers
{
    public class VacancyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Vacancy/
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.JOBTitles = (from item in db.JOBs
                                    select item.JobTitle).ToList();
            return View(db.Vacancies.ToList());
        }

        //----------------------------- Create New Vacancy ----------------------------------------------------//

        [HttpPost]
        public ActionResult CreateNewVacancy(Vacancy model)
        {
            string result = "";
            try
            {
                List<JOB> output = new List<JOB>();
                output = (from c in db.JOBs
                                    where c.JobTitle == model.publishedBy
                                    select c).ToList();
                if (output.Count != 0)
                {
                    model.publishedDate = DateTime.Now;
                    model.job = output[0];
                    model.publishedBy = User.Identity.Name;
                    db.Vacancies.Add(model);
                    db.SaveChanges();
                   
                    if (CreateVacancyFolder(model.job.JobTitle, model.VacancyId.ToString()) == "New_VacancyFolderCreated" || CreateVacancyFolder(model.job.JobTitle, model.VacancyId.ToString()) == "VacancyFolderExist")
                    {
                        result = "NewVacancyCreated_Successfully";
                    }
                    else
                    {
                        result = "NewVacancyCeated";
                    }
                }
                else
                {
                    result = "JobTitle_Not_Exsist";
                }
            }
            catch (Exception ex)
            {
                result = "ErrorOccured";
                ErrorLog.CreateLog(ex);
            }
            return Json(result); 
        }
      
        //-------------------------------------------------------------------------------------------------//

        //------------------ Delete Vacancy ---------------------------------------//
        [HttpPost]
        public ActionResult DeleteVacancy(Vacancy model)
        {
            string result = "";
            try
            {
                Vacancy vacancy = db.Vacancies.Find(model.VacancyId);

                string jobTitle = vacancy.job.JobTitle;
                string folderName = vacancy.VacancyId.ToString();

                db.Vacancies.Remove(vacancy);
                db.SaveChanges();

                if (DeleteVacancyFolder(jobTitle, model.VacancyId.ToString()) == "VacancyFolderDeleted" || DeleteVacancyFolder(jobTitle, model.VacancyId.ToString()) == "VacancyFolderNotExist")
                {
                    result = "VacancyDetails_Successfullly_Deleted";
                }
                else if (DeleteVacancyFolder(model.job.JobTitle, model.VacancyId.ToString()) == "ExceptionOccured")
                {
                    result = "VacancyDetails_Deleted";
                }

            }
            catch (Exception ex)
            {
                result = "ErrorOccured";
                ErrorLog.CreateLog(ex);
            }

            return Json(result);



        }
        //---------------------------------------------------------------------//

        
        //----------------------------- Save Vacancy Details  -------------------------------------------------//
        [HttpPost]
        public ActionResult SaveVacancyDetails(Vacancy model)
        {
            string result = "";
            try
            {
                model.publishedDate = DateTime.Now;
                List<JOB> output = (from c in db.JOBs
                                    where c.JobTitle == model.publishedBy
                                    select c).ToList();
                model.job = output[0];
                model.publishedBy = User.Identity.Name;
                model.JOBId = output[0].JOBId;
                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    result = "VacancyDetails_Successfullly_Saved";
                }
                else
                {
                    result = "VacancyDetails_Not_Saved";
                }
            }
            catch (Exception ex)
            {
                result = "ErrorOccured";
                ErrorLog.CreateLog(ex);
            }
            return Json(result);
        }
        //-------------------------------------------------------------------------------------------------//

        //------------- Create Vacancy Folder ---------------------------------------------//
        public string CreateVacancyFolder(string JobFolderName, string folderName)
        {
            string result = "";
            try
            {
                string pathString = Path.Combine(Server.MapPath("~/UploadCV/"), JobFolderName, folderName);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                    result = "New_VacancyFolderCreated";
                }
                else
                {
                    result = "VacancyFolderExist";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.CreateLog(ex);
                result = "ExceptionOccured";
            }
            return result;          
        }

        
        //------------- Delete Vacancy Folder ---------------------------------------------//
        public string DeleteVacancyFolder(string JobFolderName, string folderName)
        {
            string result = "";
            try
            {
                string pathString = Path.Combine(Server.MapPath("~/UploadCV/"), JobFolderName, folderName);
                if (Directory.Exists(pathString))
                {
                    Directory.Delete(pathString, true);
                    result = "VacancyFolderDeleted";
                }
                else
                {
                    result = "VacancyFolderNotExist";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.CreateLog(ex);
                result = "ExceptionOccured";
            }
            return result;
        }


        //--------------------- Filter Vacancy ------------------------------------//
        [HttpPost]
        public ActionResult FilterVacancy(Vacancy model)
        {
            List<Vacancy> vacancy = new List<Vacancy>();
            try
            {
                if (model.publishedBy != null)
                {
                    vacancy = (from c in db.Vacancies
                            where c.job.JobTitle.StartsWith(model.publishedBy)
                            select c).ToList();
                }
                else
                {
                    vacancy = (from c in db.Vacancies
                            select c).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.CreateLog(ex);
            }
            return Json(vacancy);
        }
        //----------------------------------------------------------------------//




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
