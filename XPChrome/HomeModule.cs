using Nancy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XPChrome
{
    public class TestModule : NancyModule
    {
        private StudyList sl = new StudyList();
        public TestModule()
            : base("/test")
        {
            Get["/"] = parameters =>
            {
                string url = this.Request.Query["requesturl"];
                return Response.AsRedirect(url);
            };
        }
    }

    public class HomeModule : NancyModule
    {
        private string serverIp = "47.96.78.170";
        private string serverPort = "8080";
        private string LocalPath = @"E:\chuchaiwenjian\zhongkeyuan\shuguangtestimage\ct";//"C:\\Users\\JPDevelopment\\Desktop\\t1";
        private StudyList sl = new StudyList();
        private StudyMetadata sm = new StudyMetadata();
        private StudyURI su = new StudyURI();
    
        public HomeModule():base("/medical-care-wado/aets/DCM4CHEE")
        {
            #region 测试代码
            //Get["/"] = x => "Hello World!";//http://34.224.187.57:3000/viewer/1.3.6.1.4.1.25403.345050719074.3824.20170126084207.4
            //Get["/"] = x => HttpUtils.HttpGet("http://123457:8080/medical-care-wado/aets/DCM4CHEE/rs/studies/1.2.840.113619.186.21217482123183196.20171012080150593.345/metadata","");
            //("http://47.96.78.170:8080/medical-care-wado/dcm/unfreeze?StudyInstanceUID=1.2.840.113619.186.21217482123183196.20171012080150593.345&HospitalID=66666", "");//
            //Get["/GetPerson/{id}/{name}"] = parameters =>
            //{
            //    return string.Format("PersonID:{0},Name:{1}",parameters.id,parameters.name);
            //};
            //Get["/medical-care-wado/dcm/unfreeze?StudyInstUID={uid}&HospitalID={hiscode}"] = parameters =>
            //{
            //    return string.Format("PersonID:{0},Name:{1}", parameters.id, parameters.name);
            //};
            //Get["/medical-care-wado/dcm/unfreeze/StudyInstanceUID={uid}&HospitalID={hiscode}"] = parameters => 
            //{
            //    string url = string.Format("http://{0}:{1}/medical-care-wado/dcm/unfreeze?StudyInstanceUID={2}&HospitalID={3}", serverIp, serverPort, parameters.uid, parameters.hiscode);
            //    return HttpUtils.HttpGet(url,"");
            //};
            #endregion
            
            Get["/rs/studies"] = parameters =>
            {
               // MessageBox.Show("11");
                ReadConfig();
                string str_rel = sl.getStudyList(LocalPath);
          //      MessageBox.Show(str_rel);
                Response.Context.Request.Form("test");
                return Response.AsText(str_rel).WithContentType("application/json").WithStatusCode(HttpStatusCode.OK);
                
            };

            Get["/rs/studies/{studyUID}/metadata"] = parameters =>
            {
                ReadConfig();
                string studyInstUid = parameters.studyUID;
                //string studyInstUid = "1.2.840.113619.186.21217482123183196.20171012080150593.345";
                string str_rel = sm.retrieveStudyMetadataASJSON(LocalPath + "\\" + studyInstUid);
                //return Response.AsText(str_rel).WithContentType("application/json").WithStatusCode(HttpStatusCode.OK);
                return Response.AsText(str_rel).WithContentType("application/json").WithStatusCode(HttpStatusCode.OK);
            };

            Get["/wado"] = parameters => 
            {
                ReadConfig();
                su.rootPath = LocalPath;
                su.studyInstanceUid = this.Request.Query["studyUID"];//parameters.studyUID;
                su.seriesInstanceUid = this.Request.Query["seriesUID"];// parameters.seriesUID;
                su.sopInstanceUid = this.Request.Query["objectUID"];// parameters.objectUID;
                string contentType = this.Request.Query["contentType"];
                //return su.getDicomFile();
                Response.Context.Response = new Nancy.Response();
                Response.Context.Response.Headers = new Dictionary<string, string>();
               Response.Context.Response.Headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin ", "*"));
                return Response.FromStream(su.getDicomFile(), contentType).WithStatusCode(HttpStatusCode.OK);
            };
            //Get["/studies?includefield=00081030%2C00080060"] = parameters =>
            //{
            //    return sl.getStudyList();
            //};
        }

        private string reString(String str){
            return str;
        }
        private bool b_flag = true;
        private void ReadConfig()
        {
            if (b_flag)
            {
                Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //根据Key读取<add>元素的Value
                LocalPath = config.AppSettings.Settings["rootPath"].Value;
                b_flag = false;
            }
        }
    }
}
