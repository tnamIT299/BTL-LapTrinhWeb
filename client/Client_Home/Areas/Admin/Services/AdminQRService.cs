using Microsoft.AspNetCore.Mvc;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.IO;
using iTextSharp.text.pdf.qrcode;
using iTextSharp.text.pdf;
using ZXing.Common;

namespace Client_Home.Areas.Admin.Services
{
    [Area("Admin")]
    public class AdminQRService : Controller
    {
        [HttpGet]
        public void GenerateQRCode(string batchId)
        {
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 100,
                    Width = 400
                }
            };
            var pixelData = barcodeWriter.Write(batchId);

            // Create a bitmap from the pixel data
            var barcode = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var barcodeData = barcode.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            try
            {
                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, barcodeData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                barcode.UnlockBits(barcodeData);
            }

            // Create a larger image to hold the barcode and strings
            var fullImage = new Bitmap(barcode.Width, barcode.Height + 60);

            using (var graphics = Graphics.FromImage(fullImage))
            {
                // Draw the barcode onto the full image
                graphics.DrawImage(barcode, new Point(0, 30));

                using (var font = new Font("Arial", 20))
                {
                    var stringSize = graphics.MeasureString("PHMart", font);
                    var point = new Point((fullImage.Width - (int)stringSize.Width) / 2, 0);
                    graphics.DrawString("PHMart", font, Brushes.Black, point);

                    stringSize = graphics.MeasureString("Budweiser", font);
                    point = new Point((fullImage.Width - (int)stringSize.Width) / 2, fullImage.Height - 30);
                    graphics.DrawString("Budweiser", font, Brushes.Black, point);
                }
            }

            string directoryPath = "wwwroot/AdminImages/Barcodes/";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = directoryPath + batchId + ".png";
            fullImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }


        public IActionResult Index()
        {
            return View();
        }

    }
}
