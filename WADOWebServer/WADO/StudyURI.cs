using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom.Imaging;
using System.Drawing;
namespace WADOWebServer
{
    public class StudyURI
    {
        public string studyInstanceUid { get; set; }
        public string seriesInstanceUid { get; set; }
        public string sopInstanceUid { get; set; }
        public string rootPath { get; set; }
        public StudyURI() { }
        public StudyURI(string _studyInstUid, string _seriesInstUid, string _sopInstUid)
        {
            this.studyInstanceUid = _studyInstUid;
            this.seriesInstanceUid = _seriesInstUid;
            this.sopInstanceUid = _sopInstUid;
        }

        public FileStream getDicomFile()
        {
            string filePath = string.Format("{0}{1}",Path.Combine(rootPath,studyInstanceUid,seriesInstanceUid,sopInstanceUid),".dcm");
            if (File.Exists(filePath))
            {
                DicomImage imagedicom = new DicomImage(filePath);
                var image = imagedicom.RenderImage().As<Bitmap>();
                var  imagePath="test.jpg";
                image.Save(imagePath);
                return File.Open(imagePath, FileMode.Open);
            }
            else
            {
                return null;// string.Empty;
            }
        }
    }
}
