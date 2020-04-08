using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Task.Common
{
    public class Crypto
    {
        #region Pre-Initialisations for  Encryption Block
        // Use DES CryptoService with Private key pair
        private static byte[] key = { };// we are going to pass in the key portion in our method calls
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        #endregion

        public static string ConnectionString
        {
            get
            {
                //string temp1 = System.Configuration.ConfigurationManager.ConnectionStrings["Abacus_dbEntities1"].ConnectionString;
                string temp = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                if (!String.IsNullOrEmpty(temp))
                {
                    return DecryptFromBase64String(temp, "B&!m#vquxI");
                }
                else
                {
                    return null;
                }
            }
        }



        public static string ConnectionString1
        {
            get
            {
                //string temp1 = System.Configuration.ConfigurationManager.ConnectionStrings["Abacus_dbEntities1"].ConnectionString;
                string temp = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                if (!String.IsNullOrEmpty(temp))
                {
                    return temp;
                }
                else
                {
                    return null;
                }
            }
        }

        public static SqlConnection myCon()
        {
            var conn = new SqlConnection(Crypto.ConnectionString);
            return conn;

        }
        /// <summary>
        /// This Function is used to Decrypt a Base64 encrypted string.
        /// </summary>
        /// <param name="stringToDecrypt">The string value to Decrypt</param>
        /// <returns>Decrypted String</returns>
        public static string DecryptFromBase64String(string stringToDecrypt, string sEncryptionKey)
        {

            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            // Note: The DES CryptoService only accepts certain key byte lengths
            // We are going to make things easy by insisting on an 8 byte legal key length

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                // we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                // now decrypt the regular string
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return "ERROR: " + e.Message;
            }
        }

        /// <summary>
        /// This Function is used to Encrypt a Base64 string.
        /// </summary>
        /// <param name="stringToEncrypt">The value to Encrypt</param>
        /// <param name="SEncryptionKey">The Encryption Key</param>
        /// <returns>Encrypted String</returns>
        public static string EncryptToBase64String(string stringToEncrypt, string SEncryptionKey)
        {

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                // convert our input string to a byte array
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                //now encrypt the bytearray
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                // now return the byte array as a "safe for XMLDOM" Base64 String
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return "ERROR: " + e.Message;
            }
        }
    }
}