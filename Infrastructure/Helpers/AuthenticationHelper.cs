using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Helpers
{
    public static class AuthenticationHelper
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

         public static bool TimeConstantCompare(string s1, string s2)
         {
             var h1 = HashAlgorithm.Create(SecurityAlgorithms.Sha256).ComputeHash(Encoding.UTF8.GetBytes(s1));
             var h2 = HashAlgorithm.Create(SecurityAlgorithms.Sha256).ComputeHash(Encoding.UTF8.GetBytes(s2));

             int accum = 0;

             for (int i = 0; i < h1.Length; i++)
             {
                accum |= (h1[i] ^ h2[i]);
             }

             return accum == 0;
         }
    }
}