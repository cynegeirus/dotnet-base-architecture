using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Encryption;

public static class StringCipher
{
    private const int KeySize = 128;
    private const int DerivationIterations = 1000;
    private const string Password = "$3sas!sB1lgiT3kn0l0jil3ri@";
    private static readonly Random Random = new();

    [Obsolete("Obsolete")]
    public static string Encrypt(string plainText)
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
            Decrypt(res);
        }
        catch
        {
            return Encrypt(plainText);
        }

        return res.IndexOf('+') > -1 ? Encrypt(plainText) : res;
    }

    [Obsolete("Obsolete")]
    public static string Decrypt(string cipherText)
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

    [Obsolete("Obsolete")]
    private static byte[] Generate256BitsOfRandomEntropy()
    {
        var randomBytes = new byte[16];
        using var rngCsp = new RNGCryptoServiceProvider();
        rngCsp.GetBytes(randomBytes);

        return randomBytes;
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

    public static string RandomString(int length)
    {
        const string chars = "AbCdEfGhIjKlMnOpQrStUvWxYz0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}