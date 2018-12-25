﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
{
    /// <summary>DICOM Value Representation</summary>
    public sealed class DicomVR
    {
        private const byte PadZero = 0x00;

        private const byte PadSpace = 0x20;

        private DicomVR()
        {
        }

        /// <summary>Code used to represent VR.</summary>
        public string Code { get; private set; }

        /// <summary>Descriptive name of VR.</summary>
        public string Name { get; private set; }

        /// <summary>Value is a string.</summary>
        public bool IsString { get; private set; }

        /// <summary>String is encoded using the specified character set.</summary>
        public bool IsStringEncoded { get; private set; }

        /// <summary>Length field of value is a 16-bit short integer.</summary>
        public bool Is16bitLength { get; private set; }

        /// <summary>Value can contain multiple items.</summary>
        public bool IsMultiValue { get; private set; }

        /// <summary>Byte value used to pad value to even length.</summary>
        public byte PaddingValue { get; private set; }

        /// <summary>Maximum length of a single value.</summary>
        public uint MaximumLength { get; private set; }

        /// <summary>Size of each individual value unit for fixed length value types.</summary>
        public int UnitSize { get; private set; }

        /// <summary>Number of bytes to swap when changing endian representation of value. Usually equal to the <see cref="UnitSize"/>.</summary>
        public int ByteSwap { get; private set; }

        /// <summary>Type used to represent VR value.</summary>
        public Type ValueType { get; private set; }

        /// <summary>
        /// Gets a string representation of this VR.
        /// </summary>
        /// <returns>VR code</returns>
        public override string ToString()
        {
            return Code;
        }

        /// <summary>
        /// Get VR for given string value.
        /// </summary>
        /// <param name="vr">String representation of VR</param>
        /// <returns>VR</returns>
        public static DicomVR Parse(string vr)
        {
            bool valid;
            DicomVR result = TryParse(vr, out valid);
            if (!valid) throw new DicomDataException(string.Format("Unknown VR: '{0}'", vr));
            return result;
        }

        /// <summary>
        /// Try to get VR for given string value.
        /// </summary>
        /// <param name="vr">String representation of VR</param>
        /// <param name="result">VR code</param>
        /// <returns>true if VR was successfully parsed, false otherwise</returns>
        public static bool TryParse(string vr, out DicomVR result)
        {
            bool valid;
            result = TryParse(vr, out valid);
            return valid;
        }

        private static DicomVR TryParse(string vr, out bool valid)
        {
            valid = true;
            switch (vr)
            {
                case "NONE":
                    return DicomVR.NONE;
                case "AE":
                    return DicomVR.AE;
                case "AS":
                    return DicomVR.AS;
                case "AT":
                    return DicomVR.AT;
                case "CS":
                    return DicomVR.CS;
                case "DA":
                    return DicomVR.DA;
                case "DS":
                    return DicomVR.DS;
                case "DT":
                    return DicomVR.DT;
                case "FD":
                    return DicomVR.FD;
                case "FL":
                    return DicomVR.FL;
                case "IS":
                    return DicomVR.IS;
                case "LO":
                    return DicomVR.LO;
                case "LT":
                    return DicomVR.LT;
                case "OB":
                    return DicomVR.OB;
                case "OD":
                    return DicomVR.OD;
                case "OF":
                    return DicomVR.OF;
                case "OW":
                    return DicomVR.OW;
                case "PN":
                    return DicomVR.PN;
                case "SH":
                    return DicomVR.SH;
                case "SL":
                    return DicomVR.SL;
                case "SQ":
                    return DicomVR.SQ;
                case "SS":
                    return DicomVR.SS;
                case "ST":
                    return DicomVR.ST;
                case "TM":
                    return DicomVR.TM;
                case "UC":
                    return DicomVR.UC;
                case "UI":
                    return DicomVR.UI;
                case "UL":
                    return DicomVR.UL;
                case "UN":
                    return DicomVR.UN;
                case "UR":
                    return DicomVR.UR;
                case "US":
                    return DicomVR.US;
                case "UT":
                    return DicomVR.UT;
                default:
                    valid = false;
                    return DicomVR.NONE;
            }
        }

        /// <summary>Implicit VR in Explicit VR context</summary>
        internal static readonly DicomVR Implicit = new DicomVR();

        /// <summary>No VR</summary>
        public static readonly DicomVR NONE = new DicomVR
                                                  {
                                                      Code = "NONE",
                                                      Name = "No Value Representation",
                                                      IsString = false,
                                                      IsStringEncoded = false,
                                                      Is16bitLength = false,
                                                      IsMultiValue = false,
                                                      PaddingValue = PadZero,
                                                      MaximumLength = 0,
                                                      UnitSize = 0,
                                                      ByteSwap = 0,
                                                      ValueType = typeof(object)
                                                  };

        /// <summary>Application Entity</summary>
        public static readonly DicomVR AE = new DicomVR
                                                {
                                                    Code = "AE",
                                                    Name = "Application Entity",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Age String</summary>
        public static readonly DicomVR AS = new DicomVR
                                                {
                                                    Code = "AS",
                                                    Name = "Age String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 4,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Attribute Tag</summary>
        public static readonly DicomVR AT = new DicomVR
                                                {
                                                    Code = "AT",
                                                    Name = "Attribute Tag",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(DicomTag)
                                                };

        /// <summary>Code String</summary>
        public static readonly DicomVR CS = new DicomVR
                                                {
                                                    Code = "CS",
                                                    Name = "Code String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Date</summary>
        public static readonly DicomVR DA = new DicomVR
                                                {
                                                    Code = "DA",
                                                    Name = "Date",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 8,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(DateTime)
                                                };

        /// <summary>Decimal String</summary>
        public static readonly DicomVR DS = new DicomVR
                                                {
                                                    Code = "DS",
                                                    Name = "Decimal String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(decimal)
                                                };

        /// <summary>Date Time</summary>
        public static readonly DicomVR DT = new DicomVR
                                                {
                                                    Code = "DT",
                                                    Name = "Date Time",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 26,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(DateTime)
                                                };

        /// <summary>Floating Point Double</summary>
        public static readonly DicomVR FD = new DicomVR
                                                {
                                                    Code = "FD",
                                                    Name = "Floating Point Double",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 8,
                                                    UnitSize = 8,
                                                    ByteSwap = 8,
                                                    ValueType = typeof(double)
                                                };

        /// <summary>Floating Point Single</summary>
        public static readonly DicomVR FL = new DicomVR
                                                {
                                                    Code = "FL",
                                                    Name = "Floating Point Single",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(float)
                                                };

        /// <summary>Integer String</summary>
        public static readonly DicomVR IS = new DicomVR
                                                {
                                                    Code = "IS",
                                                    Name = "Integer String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 12,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(int)
                                                };

        /// <summary>Long String</summary>
        public static readonly DicomVR LO = new DicomVR
                                                {
                                                    Code = "LO",
                                                    Name = "Long String",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    //IsMultiValue = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 64,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Long Text</summary>
        public static readonly DicomVR LT = new DicomVR
                                                {
                                                    Code = "LT",
                                                    Name = "Long Text",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 10240,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Other Byte</summary>
        public static readonly DicomVR OB = new DicomVR
                                                {
                                                    Code = "OB",
                                                    Name = "Other Byte",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(byte[])
                                                };

        /// <summary>Other Double</summary>
        public static readonly DicomVR OD = new DicomVR
                                                {
                                                    Code = "OD",
                                                    Name = "Other Double",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 8,
                                                    ByteSwap = 8,
                                                    ValueType = typeof(double[])
                                                };

        /// <summary>Other Float</summary>
        public static readonly DicomVR OF = new DicomVR
                                                {
                                                    Code = "OF",
                                                    Name = "Other Float",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(float[])
                                                };

        /// <summary>Other Word</summary>
        public static readonly DicomVR OW = new DicomVR
                                                {
                                                    Code = "OW",
                                                    Name = "Other Word",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 2,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(ushort)
                                                };

        /// <summary>Person Name</summary>
        public static readonly DicomVR PN = new DicomVR
                                                {
                                                    Code = "PN",
                                                    Name = "Person Name",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    //IsMultiValue = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 64,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(String)
                                                };

        /// <summary>Short String</summary>
        public static readonly DicomVR SH = new DicomVR
                                                {
                                                    Code = "SH",
                                                    Name = "Short String",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    //IsMultiValue = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Signed Long</summary>
        public static readonly DicomVR SL = new DicomVR
                                                {
                                                    Code = "SL",
                                                    Name = "Signed Long",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(int)
                                                };

        /// <summary>Sequence of Items</summary>
        public static readonly DicomVR SQ = new DicomVR
                                                {
                                                    Code = "SQ",
                                                    Name = "Sequence of Items",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 0,
                                                    ByteSwap = 0,
                                                    //ValueType = typeof(IList<DicomDataset>)
                                                };

        /// <summary>Signed Short</summary>
        public static readonly DicomVR SS = new DicomVR
                                                {
                                                    Code = "SS",
                                                    Name = "Signed Short",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 2,
                                                    UnitSize = 2,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(short)
                                                };

        /// <summary>Short Text</summary>
        public static readonly DicomVR ST = new DicomVR
                                                {
                                                    Code = "ST",
                                                    Name = "Short Text",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 1024,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Time</summary>
        public static readonly DicomVR TM = new DicomVR
                                                {
                                                    Code = "TM",
                                                    Name = "Time",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(DateTime)
                                                };

        /// <summary>Unlimited Characters</summary>
        public static readonly DicomVR UC = new DicomVR
                                                {
                                                    Code = "UC",
                                                    Name = "Unlimited Characters",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Unique Identifier</summary>
        public static readonly DicomVR UI = new DicomVR
                                                {
                                                    Code = "UI",
                                                    Name = "Unique Identifier",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 64,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Unsigned Long</summary>
        public static readonly DicomVR UL = new DicomVR
                                                {
                                                    Code = "UL",
                                                    Name = "Unsigned Long",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(uint)
                                                };

        /// <summary>Unknown</summary>
        public static readonly DicomVR UN = new DicomVR
                                                {
                                                    Code = "UN",
                                                    Name = "Unknown",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(byte[])
                                                };

        /// <summary>Universal Resource Identifier or Universal Resource Locator (URI/URL)</summary>
        public static readonly DicomVR UR = new DicomVR
                                                {
                                                    Code = "UR",
                                                    Name = "Universal Resource Identifier or Locator",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = false,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Unsigned Short</summary>
        public static readonly DicomVR US = new DicomVR
                                                {
                                                    Code = "US",
                                                    Name = "Unsigned Short",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 2,
                                                    UnitSize = 2,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(ushort)
                                                };

        /// <summary>Unlimited Text</summary>
        public static readonly DicomVR UT = new DicomVR
                                                {
                                                    Code = "UT",
                                                    Name = "Unlimited Text",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = false,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };
    }
}
