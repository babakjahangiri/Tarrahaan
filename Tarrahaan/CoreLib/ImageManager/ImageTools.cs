using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace Tarrahaan.CoreLib.ImageManager
{
    public class ImageTools
    {

        
        public Image MakeImage(string imageFilePath)
        {
            Image image = null;

            if (!File.Exists(imageFilePath))
                return null;

            using (Stream stream = File.OpenRead(imageFilePath))
            {
                image = Image.FromStream(stream);
            }

           
            //{ image = Image.FromFile(imageFilePath); }
           return image;
   
        }

        public Image ResizeImage(Image image, int width ,int height) //Image Should be Rectage with equal H&W , Ratio is 1
        {
       
            var bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(image.HorizontalResolution,image.VerticalResolution);

            //create a new graphics object from our image and set properties
            using (var gr= Graphics.FromImage(bmPhoto))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.DrawImage(image, new Rectangle(0, 0, width, height));
            }

            // Save out to memory and get an image from it to send back out the method.
            var ms = new MemoryStream();
            bmPhoto.Save(ms, ImageFormat.Jpeg);
            bmPhoto.Dispose();
            return Image.FromStream(ms);

        }


        public Image CropImage(Image image, int height, int width)
        {
            return CropImage(image, height, width, 0, 0);
        }

        public Image CropImage(Image image, int height, int width, float startAtX, float startAtY)
        {
           
                //check the image height against our desired image height
                if (image.Height < height)
                {
                    height = image.Height;
                }

                if (image.Width < width)
                {
                    width = image.Width;
                }

                //create a bitmap window for cropping
                var bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(image.HorizontalResolution,image.VerticalResolution);
           
            //create a new graphics object from our image and set properties
            using (var gr = Graphics.FromImage(bmPhoto))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                //Make Selected Rectangle 
                var cropRect = new Rectangle(0, 0, width, height);
                //now do the crop
                gr.DrawImage(image, cropRect, startAtX, startAtY, width, height, GraphicsUnit.Pixel);
            }
 
                // Save out to memory and get an image from it to send back out the method.
                var ms = new MemoryStream();
                bmPhoto.Save(ms, ImageFormat.Jpeg);
                bmPhoto.Dispose();
 
                return Image.FromStream(ms);
        }

        public void CompressAndSave(Image img, string strFilePath)
        {

            var mybitmap = new Bitmap(img);
          //  var result = "";

            //image/bmp ,image/gif ,image/jpeg ,image/png  ,image/tiff I choice JPG becuase compress is perfect
            var myImageCodecInfo = GetEncoder(ImageFormat.Jpeg);

            var encParam = new EncoderParameter(Encoder.Quality, 100L);
            var encParams = new EncoderParameters(1) {Param = {[0] = encParam}};

            try
            {
                //Save Bitmap to file
                mybitmap.Save(strFilePath, myImageCodecInfo, encParams);
                mybitmap.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving image after compress, the error is: " + ex.Message);
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;  
            }
            return null;
        }

     
    }
}