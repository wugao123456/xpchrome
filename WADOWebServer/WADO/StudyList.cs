using Dicom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPChrome
{
    public class StudyList
    {
        private Dictionary<DicomTag, string> dict_dicomTag = new Dictionary<DicomTag, string>{
            {DicomTag.StudyDate, "DA"},
            {DicomTag.StudyTime, "TM"},
            {DicomTag.AccessionNumber, "SH"},
            {DicomTag.ModalitiesInStudy, "CS"},
            {DicomTag.StudyDescription, "LO"},
            {DicomTag.PatientName, "PN"},
            {DicomTag.PatientID, "LO"},
            {DicomTag.PatientBirthDate, "DA"},
            {DicomTag.PatientSex, "CS"},
            {DicomTag.PatientAge, "AS"},
            {DicomTag.StudyInstanceUID, "UI"},
            {DicomTag.StudyID, "SH"}
        };

        public string getStudyList(string rootPath)
        {
            string str_rel = string.Empty;
            if (Directory.Exists(rootPath))
            {
                DirectoryInfo d_root = new DirectoryInfo(rootPath);
                List<Dictionary<string, Dictionary<string, object>>> list_rel = new List<Dictionary<string, Dictionary<string, object>>>();
                foreach(DirectoryInfo directory in d_root.GetDirectories()){
                    var files = Directory.EnumerateFiles(directory.FullName, "*.dcm", SearchOption.AllDirectories);
                    if (files.Count<string>() > 0)
                    {
                        Dictionary<string, Dictionary<string, object>> dict = new Dictionary<string, Dictionary<string, object>>();
                        var dicomfile = DicomFile.Open(files.First<string>());
                        foreach (var v in dict_dicomTag)
                        {
                            try
                            {
                                string key = Convert.ToString(v.Key.Group, 16).PadLeft(4, '0').ToUpper() + Convert.ToString(v.Key.Element, 16).PadLeft(4, '0').ToUpper();
                                if (!dict.ContainsKey(key))
                                {
                                    dict.Add(key, getMapByTag(v.Value, dicomfile.Dataset.Get<string>(v.Key)));
                                }
                            }
                            catch { }
                        }
                        list_rel.Add(dict);
                    }
                }
                str_rel = JsonConvert.SerializeObject(list_rel);
            }
            
            return str_rel;
        }

        /// <summary>
        /// 单张图每个tag对应的字典
        /// </summary>
        /// <param name="v"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Dictionary<string, object> getMapByTag(string v, string value)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("vr", v);
            if (value != null)
            {
                string[] str = new string[1];
                str[0] = value;
                dict.Add("Value", str);
            }
            return dict;
        }
    }
}
