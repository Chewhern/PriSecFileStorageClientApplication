using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriSecFileStorageClient
{
    public static class MainFormHolder
    {
        public static Form myForm {get;set;}

        public static Boolean OpenMainForm { get; set; }
    }
}
