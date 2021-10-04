using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriSecFileStorageClient.Model
{
    public class MFADeviceListModel
    {
        public String Status { get; set; }

        public String[] MFADeviceID { get; set; }
    }
}
