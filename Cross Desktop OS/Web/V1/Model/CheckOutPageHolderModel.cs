using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriSecFileStorageWeb.Model
{
    public class CheckOutPageHolderModel
    {
        public String PayPalOrderID { get; set; }

        public String CheckOutPageUrl { get; set; }

        public String InvoiceID { get; set; }
    }
}
