using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf.qrcode;
using QRCoder;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminQRController : Controller
    {
        [HttpGet]
        public IActionResult GenerateQR(string data, string fileName)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            var path = Path.Combine("D:\\Laptrinhtrucquan_Web\\btl\\client\\Client_Home\\wwwroot\\AdminImages\\Barcodes\\", fileName);
            System.IO.File.WriteAllBytes(path, qrCodeImage);

            return Ok(path);
        }

    }
}
