using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO.Compression;
using System.IO;

namespace PriSecFileStorageWeb.Pages
{
    public class UserImportInformationFromZipModel : PageModel
    {
        public String[] ListOfFileNames = new String[] { };
        public String Status = "";

        public void OnGet()
        {
            ReloadFiles();
        }

        public void OnPost()
        {
            ReloadFiles();
            String File_Name = Request.Form["File_Name"].ToString();
            String RootDirectory = AppContext.BaseDirectory;
            if (File_Name != null && File_Name.CompareTo("") != 0)
            {
                try
                {
                    ZipFile.ExtractToDirectory(RootDirectory + "/Import/" + File_Name, RootDirectory + "/Other_Directory_Data/");
                    Status = "Success:The_information_or_keys_from_zip_had_been_imported";
                }
                catch (Exception exception)
                {
                    Status = exception.ToString().Replace(' ', '_');
                }
            }
            else
            {
                Status = "Error:Please_Select_A_Random_File_Name";
            }
        }

        public void ReloadFiles()
        {
            String RootDirectory = AppContext.BaseDirectory;
            String[] FileNamesFullPathArray = Directory.GetFiles(RootDirectory + "/Import/");
            String[] FileNamesArray = new string[FileNamesFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (RootDirectory + "/Import/").Length;
            while (Count < FileNamesFullPathArray.Length)
            {
                FileNamesArray[Count] = FileNamesFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            ListOfFileNames = FileNamesArray;
        }
    }
}
