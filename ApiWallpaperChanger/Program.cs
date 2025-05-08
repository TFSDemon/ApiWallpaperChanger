using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApiWallpaperChanger
{
    class Program
    {
        // Windows API to change wallpaper
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDCHANGE = 0x02;

        // API URL
        public static string apiUrl = "Api Url Here";

        static async Task Main(string[] args)
        {
            while (true)
            {
                string imagePath = await DownloadImage();
                if (imagePath != null)
                {
                    SetWallpaper(imagePath); // Set desktop wallpaper
                }

                // Wait for 30 seconds before updating again
                await Task.Delay(30000);
            }
        }

        // Download image from the API
        private static async Task<string> DownloadImage()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(apiUrl);
                    var json = JsonConvert.DeserializeObject<dynamic>(response);
                    string imageUrl = json.url;

                    // Download the image
                    byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                    // Convert byte array to image
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image originalImage = Image.FromStream(ms);

                        // Skip vertical images
                        if (originalImage.Width < originalImage.Height)
                        {
                            return null;
                        }

                        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Wallpaper.jpg");

                        // Get screen resolution
                        var screenWidth = Screen.PrimaryScreen.Bounds.Width;
                        var screenHeight = Screen.PrimaryScreen.Bounds.Height;

                        // Scale the image to fit the screen resolution
                        var scaledImage = ScaleImage(originalImage, screenWidth, screenHeight);

                        // Save the scaled image to disk
                        scaledImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                        return imagePath;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        // Scale the image to the screen resolution
        private static Image ScaleImage(Image image, int width, int height)
        {
            Bitmap scaledBitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }

            return scaledBitmap;
        }

        // Set the downloaded image as wallpaper
        private static void SetWallpaper(string imagePath)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        }
    }
}
