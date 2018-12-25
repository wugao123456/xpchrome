using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WADO.Controllers
{
    public class HomeController : Controller
    {
     //@"E:\chuchaiwenjian\zhongkeyuan\shuguangtestimage\ct";
        public ActionResult Index()
        {
            ViewBag.Message = "DICOM 实现WADO服务   采用C# ASP.NET MVC  ";
            return View();
        }

        public FileResult WADO(string contentType ,string studyUID, string seriesUID, string objectUID)
        {
            string Path = System.Configuration.ConfigurationManager.AppSettings["Path"];
            StudyURI SU = new StudyURI();
            SU.studyInstanceUid = studyUID;
            SU.seriesInstanceUid = seriesUID;
            SU.sopInstanceUid = objectUID;
            SU.rootPath = Path;
            if(contentType=="application/dicom")
            return File(SU.getDicomFile(), "application/dicom");
           else   return File(SU.getImageFile(), "image/jpg");
        }

        public ActionResult Meta(string studyUID)
        {
            string Path = System.Configuration.ConfigurationManager.AppSettings["Path"];
            StudyMetadata sm = new StudyMetadata();
            sm.studyInstanceUid = studyUID;
            var relJson = sm.retrieveStudyMetadataASJSON(Path + "\\" + studyUID);
            string str_rel = JsonConvert.SerializeObject(relJson);
            Response.Headers.Add("Connection", "keep-alive");
            Response.Output.Write(str_rel);
            Response.Flush();
            Response.End();
            return Json(relJson, JsonRequestBehavior.AllowGet);
        }
       
    }
}
