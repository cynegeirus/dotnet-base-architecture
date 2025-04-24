namespace Core.Utilities.Helpers;

public class StringHelper
{
    private static readonly Random random = new();

    public static string GenerateVerifyCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string SetNumberFilter(string phoneNumber)
    {
        var newNumber = phoneNumber;

        if (newNumber.StartsWith("+9"))
            newNumber = newNumber[2..];

        if (newNumber.StartsWith("9"))
            newNumber = newNumber[1..];

        if (newNumber.StartsWith("0"))
            newNumber = newNumber[1..];

        return newNumber;
    }

    public static long[] StringToLongArray(string value)
    {
        var result = Array.Empty<long>();

        if (!string.IsNullOrEmpty(value))
        {
            var values = value.Split(',');

            result = new long[values.Length];

            for (var i = 0; i < values.Length; i++) result[i] = long.Parse(values[i]);
        }

        return result;
    }

    public static long[] StringArrayToLongArray(string[]? value)
    {
        var result = Array.Empty<long>();

        if (value is { Length: > 0 })
        {
            result = new long[value.Length];

            for (var i = 0; i < value.Length; i++)
                if (!string.IsNullOrEmpty(value[i]))
                    result[i] = long.Parse(value[i]);
        }

        return result;
    }

    public static byte[] StringToByteArray(string value)
    {
        var result = Array.Empty<byte>();

        if (!string.IsNullOrEmpty(value))
        {
            var values = value.Split(',');

            result = new byte[values.Length];

            for (var i = 0; i < values.Length; i++) result[i] = byte.Parse(values[i]);
        }

        return result;
    }

    public static string DateToStringFormat(DateTime value)
    {
        var result = value.ToShortDateString();

        var d = result.Split('.').Length > 1 ? result.Split('.')[0] : result.Split('/')[0];
        d = d.Length == 1 ? "0" + d : d;

        var m = result.Split('.').Length > 1 ? result.Split('.')[1] : result.Split('/')[1];
        m = m.Length == 1 ? "0" + m : m;

        var y = result.Split('.').Length > 1 ? result.Split('.')[2] : result.Split('/')[2];
        result = y + "-" + m + "-" + d;

        return result;
    }
}