using QRCoder;

namespace TicketMonster.ApplicationCore.Extensions
{
    public static class QRcodeHelper
    {
        public static void GenerateAndSaveQRCodeImage(string text, string filePath)
        {   
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
         
            byte[] qrCodeImageBytes = qRCode.GetGraphic(10);

            File.WriteAllBytes(filePath, qrCodeImageBytes);
        }
    }    
}
