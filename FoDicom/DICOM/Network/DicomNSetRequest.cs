﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public class DicomNSetRequest : DicomRequest
    {
        public DicomNSetRequest(DicomDataset command)
            : base(command)
        {
        }

        public DicomNSetRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid)
            : base(DicomCommandField.NSetRequest, requestedClassUid)
        {
            SOPInstanceUID = requestedInstanceUid;
        }

        public DicomUID SOPInstanceUID
        {
            get
            {
                return Command.Get<DicomUID>(DicomTag.RequestedSOPInstanceUID);
            }
            private set
            {
                Command.Add(DicomTag.RequestedSOPInstanceUID, value);
            }
        }

        public delegate void ResponseDelegate(DicomNSetRequest request, DicomNSetResponse response);

        public ResponseDelegate OnResponseReceived;

        internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                if (OnResponseReceived != null) OnResponseReceived(this, (DicomNSetResponse)response);
            }
            catch
            {
            }
        }
    }
}
