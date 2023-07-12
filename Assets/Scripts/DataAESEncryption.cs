using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public class DataAESEncryption : IService
{
    private readonly byte[] Key;  // Ключ шифрования (можно использовать любую строку)
    private readonly byte[] Iv;  // Вектор инициализации (также может быть любой строкой)

    public DataAESEncryption(string key, string iv)
    {
        Key = Encoding.UTF8.GetBytes(key);
        Iv = Encoding.UTF8.GetBytes(iv);
    }

    public string EncryptString(string value)
    {
        byte[] encryptedValue = EncryptStringToBytes(value, Key, Iv);
        string encryptedString = Convert.ToBase64String(encryptedValue);
        return encryptedString;
    }

    public string DecryptString(string encryptedString)
    {
        byte[] encryptedValue = Convert.FromBase64String(encryptedString);
        string decryptedValue = DecryptStringFromBytes(encryptedValue, Key, Iv);
        return decryptedValue;
    }

    private byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        byte[] encrypted;
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var ms = new System.IO.MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new System.IO.StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    encrypted = ms.ToArray();
                }
            }
        }
        return encrypted;
    }

    private string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        string plaintext = null;
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var ms = new System.IO.MemoryStream(cipherText))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var sr = new System.IO.StreamReader(cs))
                    {
                        plaintext = sr.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }
}
