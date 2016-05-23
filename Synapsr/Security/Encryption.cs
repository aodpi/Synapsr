using System;

namespace Synapsr.Security
{
    public class Encryption
    {
        public static string Sha1Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", string.Empty)));
        }
    }
}