using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Erste
{
    class HashGenerator
    {
        private HashAlgorithm hashAlgorithm = SHA256.Create();

        public string ComputeHash(string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public bool VerifyHash(string input, string hash)
        {
            string calculatedHash = ComputeHash(input);
            return hash.Equals(calculatedHash);
        }
    }
}
