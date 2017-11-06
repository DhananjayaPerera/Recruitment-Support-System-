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
    public class JOBController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /JOB/

        [Authorize]
        public ActionResult Index()
        {
            return View(db.JOBs.ToList());
        }

        //----------------------------- Create New Job ----------------------------------------------------//
        [HttpPost]
        public ActionResult CreateNewJOB(JOB model)
        {
            string result = "";
            try
            {
                List<JOB> output = new List<JOB>();
                output =
                     (from c in db.JOBs
                      where c.JobTitle == model.JobTitle
                      select c).ToList();

                List<JOB> output1 = new List<JOB>();

                if (output.Count == 0)
                {
                    db.JOBs.Add(model);
                    db.SaveChanges();
                    output1 = (from c in db.JOBs select c).ToList();
                    result = "JOBCreated";
                    if (CreateJobFolder(model.JobTitle) == "New_JOBFolderCreated" || CreateJobFolder(model.JobTitle) == "JOBFolderExist")
                    {
                        result = "JOBCreated_And_JobFolderCreated";
                    }
                    else
                    {
                        result = "JOBCreated_And_JobFolderNotCreated";
                    }
                }
                else {
                    result = "JobTitleAlready_Exsist";
                }
            }
            catch (Exception ex) {
                result = "ErrorOccured";
                ErrorLog.CreateLog(ex);
            }
            return Json(result);
        }
        //-------------------------------------------------------------------------------------------------//


        //----------------------------- Save JOB Details  -------------------------------------------------//
        [HttpPost]
        public ActionResult SaveJOBDetails(JOB model)
        {
            string result = "";
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    result = "JOBDetails_Successfullly_Saved";
                }
                else
                {
                    result = "JOBDetails_Not_Saved";
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


        //------------------ Delete JOB ---------------------------------------//
        [HttpPost]       
        public ActionResult DeleteJOB(JOB model)
        {
            string result = "";
            try
            {
                JOB job = db.JOBs.Find(model.JOBId);
                string jobTitle = job.JobTitle;
                db.JOBs.Remove(job);
                db.SaveChanges();

                if (DeleteJobFolder(jobTitle) == "JOBFolderDeleted" || DeleteJobFolder(jobTitle) == "JOBFolderNotExist")
                {
                    result = "JOBDetails_Successfullly_Deleted";
                }
                else if (DeleteJobFolder(jobTitle) == "ExceptionOccured")
                {
                    result = "JOBDetails_Deleted";
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


        //--------------------- Filter JOB ------------------------------------//
        [HttpPost]
        public ActionResult FilterJOB(JOB model)
        {
            List<JOB> jobs = new List<JOB>();
            try
            {
                if (model.JobTitle != null)
                {
                    jobs = (from c in db.JOBs
                            where c.JobTitle.StartsWith(model.JobTitle)
                            select c).ToList();
                }
                else
                {
                    jobs = (from c in db.JOBs
                            select c).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.CreateLog(ex);
            }           
            return Json(jobs);
        }
        //----------------------------------------------------------------------//


        //------------- Create JOB Folder ---------------------------------------------//
        public string CreateJobFolder(string folderName)
        {
            string result = "";
            try
            {
                string pathString = Path.Combine(Server.MapPath("~/UploadCV/"), folderName);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                    result = "New_JOBFolderCreated";
                }
                else {
                    result = "JOBFolderExist";
                }
            }
            catch (Exception ex) {
                ErrorLog.CreateLog(ex);
                result = "ExceptionOccured";
            }
            return result;
        }



        //------------- Delete JOB Folder ---------------------------------------------//
        public string DeleteJobFolder(string folderName)
        {          
            string result = "";
            try
            {
                string pathString = Path.Combine(Server.MapPath("~/UploadCV/"), folderName);
                if (Directory.Exists(pathString))
                {
                    Directory.Delete(pathString, true);
                    result = "JOBFolderDeleted";
                }
                else
                {
                    result = "JOBFolderNotExist";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.CreateLog(ex);
                result = "ExceptionOccured";
            }
            return result;
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
