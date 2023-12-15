using System.Diagnostics; 
using System.Security.Cryptography; 
using System.Text; 
using static System.Convert;

namespace Cryptography.Library;
/// <summary>
/// This class is a minor variation of Mark Price's code from his book Apps and Services with .Net 7
/// It can be found here: https://github.com/markjprice/apps-services-net7/blob/main/vs4win/Chapter08/CryptographyLib/Protector.cs
/// </summary>
public static class Cryptographer
{
    private static readonly byte[] salt = Encoding.Unicode.GetBytes("ISKfu4Hh");
    private static readonly int iterations = 150_000;
    public static string Encrypt(string plainText, string key)
    {
        if (salt.Length < 8) throw new ArgumentException("Salt is too short");
        byte[] encryptedBytes;
        byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
        using (Aes aes = Aes.Create())
        {
            using (Rfc2898DeriveBytes pbkdf2 = new(
            key, salt, iterations, HashAlgorithmName.SHA256))
            {
                aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key
                aes.IV = pbkdf2.GetBytes(16); // set a 128-bit IV
            };
            using (MemoryStream ms = new())
            {
                using (ICryptoTransform transformer = aes.CreateEncryptor())
                {
                    using (CryptoStream cs = new(ms, transformer, CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        if (!cs.HasFlushedFinalBlock)
                        {
                            cs.FlushFinalBlock();
                        }
                    }
                }
                encryptedBytes = ms.ToArray();
            }
        }
        return ToBase64String(encryptedBytes);
    }
    public static string Decrypt(string cipherText, string password)
    {
        byte[] plainBytes;
        byte[] cryptoBytes = FromBase64String(cipherText);
        using (Aes aes = Aes.Create())
        {
            using (Rfc2898DeriveBytes pbkdf2 = new(
            password, salt, iterations, HashAlgorithmName.SHA256))
            {
                aes.Key = pbkdf2.GetBytes(32);
                aes.IV = pbkdf2.GetBytes(16);
            }
            using (MemoryStream ms = new())
            {
                using (ICryptoTransform transformer = aes.CreateDecryptor())
                {
                    using (CryptoStream cs = new(
                    ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                        if (!cs.HasFlushedFinalBlock)
                        {
                            cs.FlushFinalBlock();
                        }
                    }
                }
                plainBytes = ms.ToArray();
            }
        }
        return Encoding.Unicode.GetString(plainBytes);
    }
}


