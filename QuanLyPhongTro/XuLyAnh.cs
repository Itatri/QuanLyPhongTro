using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace QuanLyPhongTro
{
    public class XuLyAnh
    {
        public Bitmap XuLy(string filename)
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string targetDirectory = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;

                string imagePath = Path.Combine(targetDirectory, filename);

                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("File does not exist: " + imagePath);
                    return null;
                }

                using (Image originalImage = Image.FromFile(imagePath))
                {
                    Bitmap bitmapImage = new Bitmap(originalImage);
                    Bitmap brighterImage = AdjustBrightness(bitmapImage, 1.8f);
                    Bitmap croppedImage = CropToBlueRegion(brighterImage, 10); 
                    brighterImage.Dispose();
                    bitmapImage.Dispose();
                    return croppedImage; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; 
            }
        }


        private static Bitmap AdjustBrightness(Image image, float brightnessFactor)
        {
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(adjustedImage))
            {
                float[][] ptsArray ={
            new float[] {brightnessFactor, 0, 0, 0, 0},
            new float[] {0, brightnessFactor, 0, 0, 0},
            new float[] {0, 0, brightnessFactor, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1}
        };

                ColorMatrix colorMatrix = new ColorMatrix(ptsArray);
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                            0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }

            return adjustedImage;
        }

        private static Bitmap CropToBlueRegion(Bitmap image, int padding)
        {
            int minX = image.Width, minY = image.Height;
            int maxX = 0, maxY = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    if (IsBlue(pixelColor))
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            minX = Math.Max(minX - padding, 0);
            minY = Math.Max(minY - padding, 0);
            maxX = Math.Min(maxX + padding, image.Width - 1);
            maxY = Math.Min(maxY + padding, image.Height - 1);

            int cropWidth = maxX - minX;
            int cropHeight = maxY - minY;

            if (cropWidth <= 0 || cropHeight <= 0)
            {
                throw new ArgumentException("Crop dimensions are invalid.");
            }

            Rectangle cropRect = new Rectangle(minX, minY, cropWidth, cropHeight);
            Bitmap croppedImage = new Bitmap(cropWidth, cropHeight);

            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(image, new Rectangle(0, 0, cropWidth, cropHeight),
                            cropRect, GraphicsUnit.Pixel);
            }

            return croppedImage;
        }


        private static bool IsBlue(Color color)
        {
            return color.B > 150 && color.R < 100 && color.G < 100;
        }
    }
}
