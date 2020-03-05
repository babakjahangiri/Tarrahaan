using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Tarrahaan.CoreLib.WebSite;

namespace Tarrahaan.CoreLib.ImageManager
{
    public class ImagePath
    {
        public static void CreateFolder(string path)
        {
            // Check that the file doesn't already exist. If it doesn't exist, create
            if (!System.IO.File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
           
        }

        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
               File.Delete(path);
            }
        }

        public static void DeleteAll(string path)
        {
            //Delete All Files and Folders In Property Image path 
            //Include All Files in Thumb and All Files in Large Folder

            if (System.IO.File.Exists(path)) return;

            var dirPaths = Directory.GetDirectories(path);
            foreach (var dir in dirPaths)
            {
                var filePaths = Directory.GetFiles(dir);
                foreach (var file in filePaths)
                { File.Delete(file);}

                Directory.Delete(dir);
            }

            //Finally Delete Property Image Directory
            Directory.Delete(path);
        }

        public static void ClearTempFolder(string path)
        {
            var files = Directory.GetFiles(path);

            foreach (var fi in files.Select(file => new FileInfo(file)).Where(fi => fi.LastAccessTime < DateTime.Now.AddDays(-2)))
            {
                fi.Delete();
            }
        }
    }
}