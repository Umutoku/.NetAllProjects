
using System.Drawing;

namespace WebApp.Adapter.Services
{
    public class ImageProcess : IImageProcess
    {
        public void AddWatermark(string text, string filename, Stream imageStream)
        {
            using var img = Image.FromStream(imageStream); 

            using var graphic = Graphics.FromImage(img); // Create graphic object from image

            var font = new Font(FontFamily.GenericMonospace, 40, FontStyle.Bold, GraphicsUnit.Pixel);

            var textSize = graphic.MeasureString(text, font); // Measure the size of the text

            var color = Color.FromArgb(128, 255, 255, 255);
            var brush = new SolidBrush(color); // Create brush with color

            var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));

            graphic.DrawString(text, font, brush, position);

            img.Save("wwwroot/watermarks/" + filename); // Save the image with watermark

            img.Dispose(); 
            graphic.Dispose();
        }
    }
}
