using System.Security.Cryptography;
using System.Text;

namespace TicketMonster.ApplicationCore.Extensions;

public static class HasherExtensions
{
    public static string ToSHA256(this string origin)
    {
        byte[] sourse = Encoding.Default.GetBytes(origin);
        using var mySHA256 = SHA256.Create();
        byte[] hashValue = mySHA256.ComputeHash(sourse);
        string result = hashValue.Aggregate(string.Empty, (current, t) => current + t.ToString("x2"));
        return result.ToUpper();
    }   
}