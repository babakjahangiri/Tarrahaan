using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.CoreLib.ImageManager
{
    public enum FileFormatType { Jpg = 1, Png = 2, Tif = 3 };


    public class ImageValidation
    {

        public static bool IsFormatValid(string fileName, FileFormatType allowedFormat)
        {

            var result = false;

            var filetype = fileName.Substring(fileName.Length - 3, 3);
            var jpegType = fileName.Substring(fileName.Length - 4, 4);

            switch (allowedFormat)
            {
                case FileFormatType.Jpg:
                    if ((filetype.ToLower() == "jpg") || (jpegType.ToLower() == "jpeg"))
                        result = true;
                    break;
                case FileFormatType.Png:
                   if (filetype.ToLower() == "png")
                    result = true;
                 break;
                case FileFormatType.Tif:
                    if (filetype.ToLower() == "tif")
                        result = true;
                    break;
                default:
                    result = false;
                  break;
            }

            return result;
        }

        public static bool IsFileSizeValid(string filePath, int maxValidContentLength)
        {
            var filebytes = System.IO.File.ReadAllBytes(filePath);
            long fileSize = filebytes.Length;

            return fileSize <= maxValidContentLength;

        }

        public static bool IsFileSizeValid(string filePath, int maxValidContentLength, int minValidContentLength)
        {

           var filebytes = System.IO.File.ReadAllBytes(filePath);
           long fileSize = filebytes.Length;

           return (fileSize <= maxValidContentLength) && (fileSize >= minValidContentLength);

        }

        public static bool IsFileExist(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        public static void RemoveFile(string filePath)
        {
            System.IO.File.Delete(filePath);
        }

        public static void RenameFile(string filepath, string newName)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(filepath, newName);
        }

        public static bool IsDimensionValid(string filePath, int minWidth, int minHeight)
        {
            var chkresult = false;

            using (var tempImage = System.Drawing.Image.FromFile(filePath))
            {
                if ((tempImage.Height >= minHeight) && (tempImage.Width >= minWidth))
                    chkresult = true;
            }

            return chkresult;

        }
    }
}