using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Encryption;

public static class SecurityHelper
{
    private const int KeySize = 128;
    private const int DerivationIterations = 1000;
    private const string Password = "__TOP_SECRET_PASSWORD__";
    private const string PrivateKey = "__TOP_SECRET_KEY__";

    private static readonly Random Random = new();

    [Obsolete("Obsolete")]
    public static string EncryptText(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return string.Empty;

        var saltStringBytes = Generate256BitsOfRandomEntropy();
        var ivStringBytes = Generate256BitsOfRandomEntropy();
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using var password = new Rfc2898DeriveBytes(Password, saltStringBytes, DerivationIterations);
        var keyBytes = password.GetBytes(KeySize / 8);
        using var symmetricKey = new RijndaelManaged();
        symmetricKey.BlockSize = 128;
        symmetricKey.Mode = CipherMode.CBC;
        symmetricKey.Padding = PaddingMode.PKCS7;

        using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        var cipherTextBytes = saltStringBytes;
        cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
        cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
        memoryStream.Close();
        cryptoStream.Close();

        var res = Convert.ToBase64String(cipherTextBytes);

        try
        {
            DecryptText(res);
        }
        catch
        {
            return EncryptText(plainText);
        }

        return res.IndexOf('+') > -1 ? EncryptText(plainText) : res;
    }

    [Obsolete("Obsolete")]
    public static string DecryptText(string cipherText)
    {
        var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
        var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KeySize / 8).ToArray();
        var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KeySize / 8).Take(KeySize / 8).ToArray();
        var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip(KeySize / 8 * 2).Take(cipherTextBytesWithSaltAndIv.Length - KeySize / 8 * 2).ToArray();

        using var password = new Rfc2898DeriveBytes(Password, saltStringBytes, DerivationIterations);
        var keyBytes = password.GetBytes(KeySize / 8);
        using var symmetricKey = new RijndaelManaged();
        symmetricKey.BlockSize = 128;
        symmetricKey.Mode = CipherMode.CBC;
        symmetricKey.Padding = PaddingMode.PKCS7;
        using var decrypt = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
        using var memoryStream = new MemoryStream(cipherTextBytes);
        using var cryptoStream = new CryptoStream(memoryStream, decrypt, CryptoStreamMode.Read);
        var plainTextBytes = new byte[cipherTextBytes.Length];
        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }

    public static void EncryptFile(string inputFile, string outputFile)
    {
        using var aesAlg = Aes.Create();
        var keyBytes = Encoding.UTF8.GetBytes(PrivateKey);
        aesAlg.Key = keyBytes;
        aesAlg.GenerateIV();

        using var outputStream = new FileStream(outputFile, FileMode.Create);
        outputStream.Write(aesAlg.IV, 0, aesAlg.IV.Length);

        using var cryptoStream = new CryptoStream(outputStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write);
        using var inputStream = new FileStream(inputFile, FileMode.Open);
        inputStream.CopyTo(cryptoStream);
    }

    public static void DecryptFile(string inputFile, string outputFile)
    {
        using var aesAlg = Aes.Create();
        var keyBytes = Encoding.UTF8.GetBytes(PrivateKey);
        aesAlg.Key = keyBytes;

        using var inputStream = new FileStream(inputFile, FileMode.Open);
        var iv = new byte[aesAlg.BlockSize / 8];
        _ = inputStream.Read(iv, 0, iv.Length);

        aesAlg.IV = iv;

        using var cryptoStream = new CryptoStream(inputStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Read);
        using var outputStream = new FileStream(outputFile, FileMode.Create);
        cryptoStream.CopyTo(outputStream);
    }

    public static byte[] EncryptBytes(byte[] inputBytes)
    {
        using var aesAlg = Aes.Create();
        var keyBytes = Encoding.UTF8.GetBytes(PrivateKey);
        aesAlg.Key = keyBytes;
        aesAlg.GenerateIV();

        using var memoryStream = new MemoryStream();

        memoryStream.Write(aesAlg.IV, 0, aesAlg.IV.Length);

        using var cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(inputBytes, 0, inputBytes.Length);
        cryptoStream.FlushFinalBlock();

        return memoryStream.ToArray();
    }

    public static byte[] DecryptBytes(byte[] inputBytes)
    {
        using var aesAlg = Aes.Create();
        var keyBytes = Encoding.UTF8.GetBytes(PrivateKey);
        aesAlg.Key = keyBytes;

        using var memoryStream = new MemoryStream(inputBytes);

        var iv = new byte[aesAlg.BlockSize / 8];
        _ = memoryStream.Read(iv, 0, iv.Length);

        aesAlg.IV = iv;

        using var cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Read);
        using var decryptedStream = new MemoryStream();
        cryptoStream.CopyTo(decryptedStream);

        return decryptedStream.ToArray();
    }

    public static string Base64Encode(string plainText, int loop)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        var base64 = Convert.ToBase64String(plainTextBytes);
        base64 = RandomString(10) + base64 + RandomString(10);
        for (var i = 0; i < loop; i++)
        {
            plainTextBytes = Encoding.UTF8.GetBytes(base64);
            base64 = Convert.ToBase64String(plainTextBytes);
        }

        return base64;
    }

    public static string Base64Decode(string decodeText, int loop)
    {
        if (string.IsNullOrEmpty(decodeText))
            return string.Empty;

        byte[] base64EncodedBytes;
        for (var i = 0; i < loop; i++)
        {
            base64EncodedBytes = Convert.FromBase64String(decodeText);
            decodeText = Encoding.UTF8.GetString(base64EncodedBytes);
        }

        decodeText = decodeText.Substring(10, decodeText.Length - 10);
        decodeText = decodeText.Substring(0, decodeText.Length - 10);
        base64EncodedBytes = Convert.FromBase64String(decodeText);
        decodeText = Encoding.UTF8.GetString(base64EncodedBytes);
        return decodeText;
    }

    [Obsolete("Obsolete")]
    private static byte[] Generate256BitsOfRandomEntropy()
    {
        var randomBytes = new byte[16];
        using var rngCsp = new RNGCryptoServiceProvider();
        rngCsp.GetBytes(randomBytes);

        return randomBytes;
    }

    public static string RandomString(int length)
    {
        const string chars = "AbCdEfGhIjKlMnOpQrStUvWxYz0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string ComputeSha256Hash(byte[] fileBytes)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(fileBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}