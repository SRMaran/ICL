using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Decrypt
/// </summary>
public class Decrypt
{
    public readonly string str_decrypt = "";

    public string Login(string str_pass)
    {
        string str_value = "";

        if (str_pass != "" && str_pass != null)
        {

            str_pass = str_pass.Replace(" ", "+");
            string DecryptKey = "2017(Logicalindian_@07)";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str_pass.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str_pass.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            str_value = encoding.GetString(ms.ToArray());
            // return encoding.GetString(ms.ToArray());
            return str_value;
        }
        else
        {
            return str_value;

        }

    }

    public string Register(string str)
    {
        string str_value = "";
        if (str != "" && str != null)
        {
            string EncrptKey = "2017(Logicalindian_@07)";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            str_value = Convert.ToBase64String(ms.ToArray());
            //return Convert.ToBase64String(ms.ToArray());
            return str_value;
        }
        else
        {
            return str_value;
        }

    }
    public static string DecryptQueryString(string encryptedText, string key, string iv)
    {
        // Convert the Base64 string to a byte array
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }


}
