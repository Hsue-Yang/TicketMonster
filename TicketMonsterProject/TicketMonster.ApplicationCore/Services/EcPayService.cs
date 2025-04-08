using Microsoft.Extensions.Configuration;
using System.Web;
using TicketMonster.ApplicationCore.Extensions;
using TicketMonster.ApplicationCore.Model;
using static System.Net.WebRequestMethods;

namespace TicketMonster.ApplicationCore.Services
{
    public class EcPayService
    {
        private readonly string _MerchantID;
        private readonly string _HashKey;
        private readonly string _HashIV;
        private readonly string _ReturnURL;
        private readonly string _ClientBackURL;

        public EcPayService(IConfiguration configuration)
        {           
            _MerchantID = configuration.GetSection("EcPaySettings:MerchantID").Value;
            _HashKey = configuration.GetSection("EcPaySettings:HashKey").Value;
            _HashIV = configuration.GetSection("EcPaySettings:HashIV").Value;
            _ReturnURL = configuration.GetSection("EcPaySettings:ReturnURL").Value;
            _ClientBackURL = configuration.GetSection("EcPaySettings:ClientBackURL").Value;

        }

        public EcPayCreateOrderDto CreatePaymentOrder(int totalprice,int unitprice,int count,string name,string ticketname, string merchantradeno)
        {
            var tax = totalprice - unitprice * count;
            var result = new EcPayCreateOrderDto
            {
                MerchantID = _MerchantID,
                MerchantTradeNo = merchantradeno,
                MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                PaymentType = "aio",
                TotalAmount = totalprice,
                TradeDesc = name,
                ItemName = $"{ticketname} - ${unitprice} X {count}#Fees&Taxes ${tax}",
                ReturnURL = _ReturnURL,
                ChoosePayment = "Credit",
                EncryptType = 1,
                ClientBackURL = _ClientBackURL
            };

            var myData = new Dictionary<string, string>()
            { 
                {"MerchantID",result.MerchantID },
                {"MerchantTradeNo",result.MerchantTradeNo },
                {"MerchantTradeDate",result.MerchantTradeDate },
                {"PaymentType",result.PaymentType },
                {"TotalAmount",result.TotalAmount.ToString()},
                {"TradeDesc",result.TradeDesc},
                {"ItemName",result.ItemName},
                {"ReturnURL",result.ReturnURL},
                {"ChoosePayment",result.ChoosePayment},
                {"EncryptType",result.EncryptType.ToString() },
                {"ClientBackURL",result.ClientBackURL }
            };
            result.CheckMacValue = EcPayExtensions.BuildCheckMackValue(myData, _HashKey, _HashIV);
            return result;
        }     

        //public EcPayCreateOrderViewModel DecodeCheckMacValue(string checkMacValue, string hashKey, string hashIV, int money, string name, string ticketname)
        //{
        //    // 首先解码 CheckMacValue，并将其转换为小写
        //    string decodedCheckMacValue = HttpUtility.UrlDecode(checkMacValue.ToLower());

        //    // 计算 CheckMacValue 的原始哈希字符串
        //    string originalHashString = decodedCheckMacValue.Replace($"hashkey={hashKey.ToLower()}&", "").Replace($"hashiv={hashIV.ToLower()}", "");

        //    // 计算原始哈希字符串的 SHA-256 哈希
        //    string originalHash = originalHashString.ToSHA256();

        //    // 创建一个新的 EcPayCreateOrderViewModel 对象，将原始哈希值设置为 CheckMacValue
        //    var result = new EcPayCreateOrderViewModel
        //    {
        //        MerchantID = _MerchantID,
        //        MerchantTradeNo = $"T{DateTime.Now.Ticks}",
        //        MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
        //        PaymentType = "aio",
        //        TotalAmount = money,
        //        TradeDesc = name,
        //        ItemName = $"{ticketname}-${money}",
        //        ReturnURL = " https://90e7-114-24-162-101.ngrok-free.app /CheckOutPage/ReturnUrl",
        //        ChoosePayment = "Credit",
        //        EncryptType = 1,
        //        ClientBackURL = "https://64a3-114-24-171-98.ngrok-free.app/",
        //        CheckMacValue = originalHash.ToUpper() // 将原始哈希值转换为大写并设置为 CheckMacValue
        //    };

        //    return result;
        //}


    }
}

