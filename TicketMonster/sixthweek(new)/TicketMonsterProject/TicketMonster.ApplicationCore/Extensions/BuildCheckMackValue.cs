using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TicketMonster.ApplicationCore.Extensions;

public static class EcPayExtensions
{
    public static string BuildCheckMackValue(Dictionary<string, string> myData, string hashKey, string hashIV)
    {
        var str = string.Join("&", myData.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value}"));
        var para = $"HashKey={hashKey}&{str}&HashIV={hashIV}";
        var encodeStr = HttpUtility.UrlEncode(para).ToLower();
        var encryptStr = encodeStr.ToSHA256();
        var checkMacValue = encryptStr.ToUpper();
        return checkMacValue;
    }
}







