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
            try// Nó vậy á 
            {
                // Get the base directory and construct the image path
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Navigate back two folders and then into "AnhChuKy"
                string targetDirectory = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;

                // Specify the filename of the image located in "AnhChuKy" folder
                string imagePath = Path.Combine(targetDirectory, filename);

                // Check if the file exists
                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("File does not exist: " + imagePath);
                    return null;
                }
                MessageBox.Show(imagePath);
                // Load the original image
                using (Image originalImage = Image.FromFile(imagePath))
                {
                    // Convert Image to Bitmap
                    Bitmap bitmapImage = new Bitmap(originalImage);
                    // Increase brightness
                    Bitmap brighterImage = AdjustBrightness(bitmapImage, 1.8f);
                    // Crop the image
                    Bitmap croppedImage = CropToBlueRegion(brighterImage, 10); // 10 pixels padding
                    // Dispose of resources for brighterImage and bitmapImage
                    brighterImage.Dispose();
                    bitmapImage.Dispose();
                    //MessageBox.Show("4");
                    return croppedImage; // Return cropped image
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; // Return null on error
            }
        }


        private static Bitmap AdjustBrightness(Image image, float brightnessFactor)
        {
            // Tạo một bitmap mới với cùng kích thước ảnh gốc
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);

            // Sử dụng Graphics để vẽ lại ảnh với các thiết lập độ sáng
            using (Graphics g = Graphics.FromImage(adjustedImage))
            {
                // Tạo một ma trận màu để điều chỉnh độ sáng
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

                // Vẽ lại ảnh với độ sáng đã điều chỉnh
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                            0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }

            return adjustedImage;
        }

        private static Bitmap CropToBlueRegion(Bitmap image, int padding)
        {
            int minX = image.Width, minY = image.Height;
            int maxX = 0, maxY = 0;

            // Duyệt qua từng pixel để xác định vị trí vùng chứa màu xanh dương
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    // Kiểm tra nếu màu là màu xanh dương (tùy chỉnh theo ngưỡng RGB)
                    if (IsBlue(pixelColor))
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            // Thêm khoảng cách (padding) để cắt vùng xung quanh
            minX = Math.Max(minX - padding, 0);
            minY = Math.Max(minY - padding, 0);
            maxX = Math.Min(maxX + padding, image.Width - 1);
            maxY = Math.Min(maxY + padding, image.Height - 1);

            // Chiều rộng và chiều cao của ảnh cắt
            int cropWidth = maxX - minX;
            int cropHeight = maxY - minY;

            // Kiểm tra kích thước hợp lệ trước khi cắt
            if (cropWidth <= 0 || cropHeight <= 0)
            {
                throw new ArgumentException("Crop dimensions are invalid.");
            }

            // Cắt ảnh
            Rectangle cropRect = new Rectangle(minX, minY, cropWidth, cropHeight);
            Bitmap croppedImage = new Bitmap(cropWidth, cropHeight);

            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(image, new Rectangle(0, 0, cropWidth, cropHeight),
                            cropRect, GraphicsUnit.Pixel);
            }

            return croppedImage;
        }


        // Phương thức xác định màu có phải là xanh dương hay không (có thể điều chỉnh ngưỡng theo yêu cầu)
        private static bool IsBlue(Color color)
        {
            return color.B > 150 && color.R < 100 && color.G < 100;
        }
    }
}
