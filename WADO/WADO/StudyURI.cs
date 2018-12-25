using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
namespace WADO
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
            string filePath = string.Format("{0}{1}", Path.Combine(rootPath, studyInstanceUid, seriesInstanceUid, sopInstanceUid), ".dcm");
            if (File.Exists(filePath))
            {
                return File.Open(filePath, FileMode.Open);
            }
            else
            {
                return null;// string.Empty;
            }
        }
        public Stream getImageFile()
        {
            string filePath = string.Format("{0}{1}", Path.Combine(rootPath, studyInstanceUid, seriesInstanceUid, sopInstanceUid), ".dcm");
            if (File.Exists(filePath))
            {
                DicomImage imagedicom = new DicomImage(filePath);
                 var bitmapImage = imagedicom.RenderImage().As<Bitmap>();
                 filePath = sopInstanceUid + ".jpg";
                 bitmapImage.Save(filePath);
               // Stream BmpStream = new MemoryStream();
               // bitmapImage.Save(BmpStream,ImageFormat.Icon);
                return File.Open(filePath, FileMode.Open);
               
            }
            else
            {
                return null;// string.Empty;
            }
        }
    }
}
