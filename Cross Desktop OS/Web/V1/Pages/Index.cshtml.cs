using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace PriSecFileStorageWeb.Pages
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
            String RootDirectory = AppContext.BaseDirectory;
            String EstablishStatus = System.IO.File.ReadAllText(RootDirectory + "/Temp_Data/" + "EstablishSharedSecret.txt");
            if (EstablishStatus.Contains("Success") == true) 
            {
                ViewData["Status"] = "True";
            }
            else 
            {
                ViewData["Status"] = "False";
            }
        }
    }
}
