using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AesHelper
{
    public static void GenerateAndSaveKey()
    {
        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        File.WriteAllText("aes_key.txt", Convert.ToBase64String(aes.Key));
        File.WriteAllText("aes_iv.txt", Convert.ToBase64String(aes.IV));
    }
    public static Aes ImportKey()
    {
        Aes aes = Aes.Create();
        aes.Key = Convert.FromBase64String(File.ReadAllText("aes_key.txt"));
        aes.IV = Convert.FromBase64String(File.ReadAllText("aes_iv.txt"));
        return aes;
    }
    public static string EncryptText(string plainText, Aes aes)
    {
        using ICryptoTransform encryptor = aes.CreateEncryptor();
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }
    public static void EncryptLargeFile(string sourceFile, string destFile, Aes aes)
    {
        using FileStream fsIn = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
        using FileStream fsOut = new FileStream(destFile, FileMode.Create, FileAccess.Write);
        using ICryptoTransform encryptor = aes.CreateEncryptor();
        using CryptoStream cs = new CryptoStream(fsOut, encryptor, CryptoStreamMode.Write);
        fsIn.CopyTo(cs);
    }
    public static void DecryptLargeFile(string sourceFile, string destFile, Aes aes)
    {
        using FileStream fsIn = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
        using FileStream fsOut = new FileStream(destFile, FileMode.Create, FileAccess.Write);
        using ICryptoTransform decryptor = aes.CreateDecryptor();
        using CryptoStream cs = new CryptoStream(fsIn, decryptor, CryptoStreamMode.Read);
        cs.CopyTo(fsOut);
    }
    public static bool CompareFiles(string file1, string file2)
    {
        FileInfo f1 = new FileInfo(file1);
        FileInfo f2 = new FileInfo(file2);
        if (f1.Length != f2.Length) return false;
        using FileStream fs1 = f1.OpenRead();
        using FileStream fs2 = f2.OpenRead();
        byte[] buffer1 = new byte[1024 * 1024];
        byte[] buffer2 = new byte[1024 * 1024];
        int bytesRead;
        while ((bytesRead = fs1.Read(buffer1, 0, buffer1.Length)) > 0)
        {
            fs2.Read(buffer2, 0, buffer2.Length);
            for (int i = 0; i < bytesRead; i++)
                if (buffer1[i] != buffer2[i]) return false;
        }
        return true;
    }
}