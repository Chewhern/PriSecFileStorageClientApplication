using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PriSecFileStorageClient.Helper
{
    class SecureIDGenerator
    {
        public String GenerateUniqueString()
        {
            Byte[] CryptographicSecureData = new Byte[240];
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(CryptographicSecureData);
            int Loop = 0;
            StringBuilder stringBuilder = new StringBuilder();
            while (Loop < CryptographicSecureData.Length)
            {
                if (CryptographicSecureData[Loop] >= 48 && CryptographicSecureData[Loop] <= 57)
                {
                    stringBuilder.Append((char)CryptographicSecureData[Loop]);
                }
                else if (CryptographicSecureData[Loop] >= 65 && CryptographicSecureData[Loop] <= 90)
                {
                    stringBuilder.Append((char)CryptographicSecureData[Loop]);
                }
                else if (CryptographicSecureData[Loop] >= 97 && CryptographicSecureData[Loop] <= 122)
                {
                    stringBuilder.Append((char)CryptographicSecureData[Loop]);
                }
                Loop += 1;
            }
            if (stringBuilder.ToString().CompareTo("") != 0)
            {
                return stringBuilder.ToString();
            }
            else
            {
                return "";
            }
        }

        public String GenerateMinimumAmountOfUniqueString(int Amount)
        {
            String TestString = GenerateUniqueString();
            while (TestString.Length < Amount)
            {
                TestString += GenerateUniqueString();
            }
            return TestString;
        }
    }
}
