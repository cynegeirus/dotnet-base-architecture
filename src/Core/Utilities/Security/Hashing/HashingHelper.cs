﻿using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing;

public class HashingHelper
{
    public static void CreatePasswordHash(string? password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password!));
    }

    public static bool VerifyPasswordHash(string? password, byte[]? passwordHash, byte[]? passwordSalt)
    {
        using HMACSHA512 hmac = new(passwordSalt!);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password!));
        return !computedHash.Where((t, i) => t != passwordHash![i]).Any();
    }
}