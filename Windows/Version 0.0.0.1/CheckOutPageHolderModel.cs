using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriSecFileStorageClient
{
    public class CheckOutPageHolderModel
    {
        public String PayPalOrderID { get; set; }

        public String CheckOutPageUrl { get; set; }

        public String InvoiceID { get; set; }
    }
}
