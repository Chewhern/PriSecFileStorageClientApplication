using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASodium;
using System.IO;

namespace PriSecFileStorageWeb.Pages
{
    public class UserGenerateKeyModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            Byte[] SignedED25519PK = SodiumPublicKeyAuth.Sign(MyKeyPair.PublicKey, MyKeyPair.PrivateKey);
            Byte[] ED25519PK = MyKeyPair.PublicKey;
            Byte[] MergedED25519PKS = ED25519PK.Concat(SignedED25519PK).ToArray();
            ViewData["SK"] = Convert.ToBase64String(MyKeyPair.PrivateKey);
            ViewData["Merged_PK"] = Convert.ToBase64String(MergedED25519PKS);
            MyKeyPair.Clear();
        }

        public void OnPostGAccess() 
        {
            String SK = Request.Form["SK"].ToString();
            String Merged_PK = Request.Form["Merged_PK"].ToString();
            String Storage_ID = Request.Form["Storage_ID"].ToString();
            String Access_ID = Request.Form["Access_ID"].ToString();
            String RootDirectory = AppContext.BaseDirectory;
            if (SK.CompareTo("") != 0
                && Merged_PK.CompareTo("") != 0
                && Storage_ID.CompareTo("") != 0 && Access_ID.CompareTo("") != 0)
            {
                Byte[] MergedED25519PKS = Convert.FromBase64String(Merged_PK);
                Byte[] ED25519SK = Convert.FromBase64String(SK);
                Byte[] ED25519PK = new Byte[32];
                Byte[] SignedED25519PK = new Byte[96];
                Array.Copy(MergedED25519PKS, 0, ED25519PK, 0, 32);
                Array.Copy(MergedED25519PKS, 32, SignedED25519PK, 0, 96);
                if (Directory.Exists(RootDirectory + "/Other_Directory_Data/" + Storage_ID) == false)
                {
                    Directory.CreateDirectory(RootDirectory + "/Other_Directory_Data/" + Storage_ID);
                }
                System.IO.File.WriteAllText(RootDirectory + "/Other_Directory_Data/" + Storage_ID + "/AccessID.txt", Access_ID);
                System.IO.File.WriteAllBytes(RootDirectory + "/Other_Directory_Data/" + Storage_ID + "/SK.txt", ED25519SK);
                System.IO.File.WriteAllBytes(RootDirectory + "/Other_Directory_Data/" + Storage_ID + "/PK.txt", ED25519PK);
                System.IO.File.WriteAllBytes(RootDirectory + "/Other_Directory_Data/" + Storage_ID + "/SPK.txt", SignedED25519PK);
                SodiumSecureMemory.SecureClearString(Merged_PK);
                SodiumSecureMemory.SecureClearString(SK);
                SodiumSecureMemory.SecureClearBytes(ED25519SK);
                SodiumSecureMemory.SecureClearBytes(ED25519PK);
                SodiumSecureMemory.SecureClearBytes(SignedED25519PK);
                SodiumSecureMemory.SecureClearBytes(MergedED25519PKS);
                ViewData["Status"]="Access_has_been_granted_by_user's_confirmation";
            }
            else
            {
                ViewData["Status"]="Please_Create_Signature_Keys_and_get_access_ID_+_storage_ID_from_owner";
            }
        }
    }
}
