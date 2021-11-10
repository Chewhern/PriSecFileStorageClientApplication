using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using PriSecFileStorageWeb.Helper;

namespace PriSecFileStorageWeb.Pages
{
    public class UserChooseStorageIDModel : PageModel
    {
        public String[] ListOfStorageIDs = new String[] { };

        public void OnGet()
        {
            ReloadDirectory();
        }

        public void OnPost()
        {
            ReloadDirectory();
            String CurrentDirectory = Request.Form["Storage_ID"].ToString();
            ViewData["Current_Storage_ID"] = CurrentDirectory;
            if (CurrentDirectory != null && CurrentDirectory.CompareTo("") != 0)
            {
                DirectoryIDTempStorage.DirectoryID = CurrentDirectory;
            }
        }

        public void ReloadDirectory()
        {
            String RootDirectory = AppContext.BaseDirectory;
            String[] DirectoryIDFullPathArray = Directory.GetDirectories(RootDirectory + "/Other_Directory_Data/");
            String[] DirectoryIDArray = new string[DirectoryIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (RootDirectory + "/Other_Directory_Data/").Length;
            while (Count < DirectoryIDFullPathArray.Length)
            {
                DirectoryIDArray[Count] = DirectoryIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            ListOfStorageIDs = DirectoryIDArray;
        }
    }
}
