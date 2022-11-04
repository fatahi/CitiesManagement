using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework
{
    public static class Encryption
    {
        public static string HashSHA256(string input)
        {
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string HashPasswordWithSalt(string password, string salt)
        {
            return HashSHA256(password + salt);
        }

        static string _key = "MgZAT9UQBUaC7EuCVABJco/kpL+uRQA1b63kU4mrOPK=";
        static readonly char[] padding = { '=' };

        public static string Encrypt(string data)
        {
            byte[] toEncryptArry = Encoding.UTF8.GetBytes(data);
            byte[] keyArry = Convert.FromBase64String(_key);

            var aes = new AesCryptoServiceProvider
            {
                Key = keyArry,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.ISO10126
            };
            ICryptoTransform cTransform = aes.CreateEncryptor();
            byte[] encrypted = cTransform.TransformFinalBlock(toEncryptArry, 0, toEncryptArry.Length);
            string encryptCode = Convert.ToBase64String(encrypted);

            encryptCode = encryptCode.TrimEnd(padding).Replace('+', '-').Replace('/', '_');
            return encryptCode;
        }
        public static string Decrypt(string data)
        {
            string incoming = data.Replace('_', '/').Replace('-', '+');
            switch (data.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }


            byte[] toDecryptArry = Convert.FromBase64String(incoming);
            byte[] keyArry = Convert.FromBase64String(_key);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                Key = keyArry,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.ISO10126
            };
            ICryptoTransform cTransform = aes.CreateDecryptor();
            byte[] decrypted = cTransform.TransformFinalBlock(toDecryptArry, 0, toDecryptArry.Length);
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
