using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriSecFileStorageClient.Model
{
    public class MFAUploadFilesModel
    {
        public String ClientPathID { get; set; }

        public String CipheredSignedDirectoryID { get; set; }

        public String FirstMFADeviceID { get; set; }

        public String SecondMFADeviceID { get; set; }

        public int LastFileIndex { get; set; }

        public String SignedSignedRandomChallenge { get; set; }

        public String SignedUniqueFileName { get; set; }

        public String CipheredSignedFileContent { get; set; }
    }
}
