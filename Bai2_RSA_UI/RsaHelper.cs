using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class RsaHelper
{
    // 1. Sinh cặp khóa RSA 2048-bit và lưu ra 2 file riêng biệt
    public static void GenerateAndSaveKeys()
    {
        using RSA rsa = RSA.Create(2048);
        File.WriteAllText("rsa_public.txt", Convert.ToBase64String(rsa.ExportRSAPublicKey()));
        File.WriteAllText("rsa_private.txt", Convert.ToBase64String(rsa.ExportRSAPrivateKey()));
    }

    // 2. Nạp khóa công khai (Dùng để Mã hóa)
    public static RSA ImportPublicKey()
    {
        RSA rsa = RSA.Create();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(File.ReadAllText("rsa_public.txt")), out _);
        return rsa;
    }

    // 3. Nạp khóa bí mật (Dùng để Giải mã)
    public static RSA ImportPrivateKey()
    {
        RSA rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(File.ReadAllText("rsa_private.txt")), out _);
        return rsa;
    }

    // 4. Mã hóa văn bản
    public static string EncryptText(string plainText, RSA rsaPublic)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainText);
        // Padding Pkcs1 là chuẩn phổ biến nhất cho bài tập RSA
        byte[] encryptedData = rsaPublic.Encrypt(data, RSAEncryptionPadding.Pkcs1);
        return Convert.ToBase64String(encryptedData);
    }

    // 5. Giải mã văn bản
    public static string DecryptText(string encryptedBase64, RSA rsaPrivate)
    {
        byte[] dataToDecrypt = Convert.FromBase64String(encryptedBase64);
        byte[] decryptedData = rsaPrivate.Decrypt(dataToDecrypt, RSAEncryptionPadding.Pkcs1);
        return Encoding.UTF8.GetString(decryptedData);
    }
}