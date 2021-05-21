using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriSecFileStorageClient
{
    public class UploadFilesModel
    {
        public String ClientPathID { get; set; } 

        public String SignedUserID { get; set; }

        public String CipheredSignedDirectoryID { get; set; }
        
        public String SignedSignedRandomChallenge { get; set; }

        public String SignedUniqueFileName { get; set; } 
        
        public String SignedCipheredSignedFileContent { get; set; }
    }
}
