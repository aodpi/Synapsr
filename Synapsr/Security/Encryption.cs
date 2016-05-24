using System;
using System.Collections.Generic;
using System.Linq;

namespace Synapsr.Security
{
    public class Encryption
    {
        /// <summary>
        /// SHA1 Encryption with some tricks.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Sha1Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            var finalstr = BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", string.Empty);
            var finalstrbytes = System.Text.Encoding.UTF8.GetBytes(finalstr);
            var finalhash = Convert.ToBase64String(finalstrbytes.Reverse().ToArray());
            return finalhash;
        }
    }
}