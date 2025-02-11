﻿using HospitalMiddleware.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace HospitalMiddleware.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionService()
        {
            _key = Encoding.UTF8.GetBytes("EncryptionKey123");
            _iv = Encoding.UTF8.GetBytes("EncryptionIV1234");
        }


        //Yet to figure out why this Encrypt method does not work
        //public string Encrypt(string plainText)
        //{
        //    using var aesAlg = Aes.Create();
        //    aesAlg.Key = _key;
        //    aesAlg.IV = _iv;

        //    var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        //    using var msEncrypt = new MemoryStream();
        //    using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        //    using var swEncrypt = new StreamWriter(csEncrypt);
        //    swEncrypt.Write(plainText);
        //    return Convert.ToBase64String(msEncrypt.ToArray());
        //}

        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                            
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                       
                }
               
            }
                
        }


        public string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            using var aesAlg = Aes.Create();
            aesAlg.Key = _key;
            aesAlg.IV = _iv;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(cipherBytes);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
