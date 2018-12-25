﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpeg2000LossyCodec : DicomJpeg2000Codec
    {
        public override DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.JPEG2000Lossy;
            }
        }

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpeg2000NativeCodec.Encode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpeg2000Parameters());
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpeg2000NativeCodec.Decode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpeg2000Parameters());
        }
    }
}
