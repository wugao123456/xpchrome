﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public interface IDicomCEchoProvider
    {
        DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request);
    }
}
