using Dicom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XPChrome
{
    public class StudyMetadata
    {
        public string studyInstanceUid{get;set;}
        public string seriesInstanceUid{get;set;}

        public void test()
        {
            MessageBox.Show(dict_dicomTag.Count.ToString());
        }

        #region list_dicomTag
        private Dictionary<DicomTag, string> dict_dicomTag = new Dictionary<DicomTag, string>{
            {DicomTag.SpecificCharacterSet, "CS"},
            {DicomTag.ImageType, "CS"},
            {DicomTag.SOPClassUID, "UI"},
            {DicomTag.SOPInstanceUID,"UI"},
            {DicomTag.StudyDate, "DA"},
            {DicomTag.SeriesDate, "DA"},
            {DicomTag.StudyTime, "TM"},
            {DicomTag.SeriesTime, "TM"},
            {DicomTag.AccessionNumber, "SH"},
            {DicomTag.Modality, "CS"},
            {DicomTag.Manufacturer, "LO"},
            {DicomTag.InstitutionName, "LO"},
            {DicomTag.InstitutionAddress, "ST"},
            {DicomTag.ReferringPhysicianName, "PN"},
            {DicomTag.StationName, "SH"},
            {DicomTag.StudyDescription, "LO"},
            {DicomTag.SeriesDescription, "LO"},
            {DicomTag.PerformingPhysicianName, "PN"},
            {DicomTag.ManufacturerModelName, "LO"},
            {DicomTag.IrradiationEventUID, "UI"},
            {DicomTag.PatientName, "PN"},
            {DicomTag.PatientID, "LO"},
            {DicomTag.PatientBirthDate, "DA"},
            {DicomTag.PatientSex, "CS"},
            {DicomTag.PatientAge, "AS"},
            {DicomTag.PregnancyStatus, "US"},
            {DicomTag.BodyPartExamined, "CS"},
            {DicomTag.SliceThickness, "DS"},
            {DicomTag.KVP, "DS"},
            {DicomTag.DataCollectionDiameter, "DS"},
            {DicomTag.DeviceSerialNumber, "LO"},
            {DicomTag.SoftwareVersions, "LO"},
            {DicomTag.ProtocolName, "LO"},
            {DicomTag.ReconstructionDiameter, "DS"},
            {DicomTag.DistanceSourceToDetector, "DS"},
            {DicomTag.DistanceSourceToPatient, "DS"},
            {DicomTag.GantryDetectorTilt, "DS"},
            {DicomTag.TableHeight, "DS"},
            {DicomTag.RotationDirection, "CS"},
            {DicomTag.ExposureTime, "IS"},
            {DicomTag.XRayTubeCurrent, "IS"},
            {DicomTag.FilterType, "SH"},
            {DicomTag.GeneratorPower, "IS"},
            {DicomTag.FocalSpots, "DS"},
            {DicomTag.DateOfLastCalibration, "DA"},
            {DicomTag.TimeOfLastCalibration, "TM"},
            {DicomTag.ConvolutionKernel, "SH"},
            {DicomTag.PatientPosition, "CS"},
            {DicomTag.SingleCollimationWidth, "FD"},
            {DicomTag.TotalCollimationWidth, "FD"},
            {DicomTag.TableSpeed, "FD"},
            {DicomTag.TableFeedPerRotation, "FD"},
            {DicomTag.SpiralPitchFactor, "FD"},
            {DicomTag.DataCollectionCenterPatient, "FD"},
            {DicomTag.ReconstructionTargetCenterPatient, "FD"},
            {DicomTag.ExposureModulationType, "CS"},
            {DicomTag.EstimatedDoseSaving, "FD"},
            {DicomTag.CTDIvol, "FD"},
            {DicomTag.CalciumScoringMassFactorDevice, "FL"},
            {DicomTag.StudyInstanceUID, "UI"},
            {DicomTag.SeriesInstanceUID, "UI"},
            {DicomTag.StudyID, "SH"},
            {DicomTag.SeriesNumber, "IS"},
            {DicomTag.AcquisitionNumber, "IS"},
            {DicomTag.InstanceNumber, "IS"},
            {DicomTag.ImagePositionPatient, "DS"},
            {DicomTag.ImageOrientationPatient, "DS"},
            {DicomTag.FrameOfReferenceUID, "UI"},
            {DicomTag.PositionReferenceIndicator, "LO"},
            {DicomTag.SliceLocation, "DS"},
            {DicomTag.SamplesPerPixel, "US"},
            {DicomTag.PhotometricInterpretation, "CS"},
            {DicomTag.NumberOfFrames, "IS"},
            {DicomTag.Rows, "US"},
            {DicomTag.Columns, "US"},
            {DicomTag.PixelSpacing, "DS"},
            {DicomTag.BitsAllocated, "US"},
            {DicomTag.BitsStored, "US"},
            {DicomTag.HighBit, "US"},
            {DicomTag.PixelRepresentation, "US"},
            {DicomTag.SmallestImagePixelValue, "US"},
            {DicomTag.LargestImagePixelValue, "US"},
            {DicomTag.WindowCenter, "DS"},
            {DicomTag.WindowWidth, "DS"},
            {DicomTag.RescaleIntercept, "DS"},
            {DicomTag.RescaleSlope, "DS"},
            {DicomTag.RescaleType, "LO"},
            {DicomTag.WindowCenterWidthExplanation, "LO"},
            {DicomTag.RequestedProcedureDescription, "LO"}
        };
        #endregion

        public StudyMetadata(string _studyInstanceUid, String _seriesInstanceuid)
        {
            this.studyInstanceUid = _studyInstanceUid;
            this.seriesInstanceUid = _seriesInstanceuid;
        }
        public StudyMetadata(string _studyInstanceUid)
        {
            this.studyInstanceUid = _studyInstanceUid;
        }
        public StudyMetadata()
        {
        }
        public string retrieveStudyMetadataASJSON(string studyPath)
        {
            //string studyPath = "C:\\Users\\JPDevelopment\\Desktop\\t1";
            List<Dictionary<string, object>> list_rel = getMapByStudyPath(studyPath);
            string str_rel = JsonConvert.SerializeObject(list_rel);
            return str_rel;
        }

        private List<Dictionary<string, object>> getMapByStudyPath(string studyPath)
        {
            List<Dictionary<string, object>> list_rel = new List<Dictionary<string, object>>();
            try
            {
                var files = Directory.EnumerateFiles(studyPath, "*.dcm", SearchOption.AllDirectories);
                foreach (string filename in files)
                {
                    var dcm = DicomFile.Open(filename);//,DicomEncoding.GetEncoding("GB18030"));
                    Dictionary<string, object> dict = getTagByList(dcm.Dataset, dict_dicomTag);
                    list_rel.Add(dict);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return list_rel;
        }

        private Dictionary<string, object> getTagByList(DicomDataset dicomDataSet, Dictionary<DicomTag, string> tagClassList)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (var tagclass in tagClassList)
            {
                try
                {
                    string key = Convert.ToString(tagclass.Key.Group, 16).PadLeft(4, '0').ToUpper() + Convert.ToString(tagclass.Key.Element, 16).PadLeft(4, '0').ToUpper();
                    if(!dict.ContainsKey(key))
                    { 
                        dict.Add(key, getMapByTag(tagclass.Value, dicomDataSet.Get<string>(tagclass.Key))); 
                    }
                    else
                    {
                        MessageBox.Show(tagclass.Key.Element.ToString());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace.ToString());
                }
            }
            return dict;
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
