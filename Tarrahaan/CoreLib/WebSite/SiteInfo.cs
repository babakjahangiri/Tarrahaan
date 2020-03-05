using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.CoreLib.WebSite
{
    public static class SiteInfo
    {
        public static string SiteName()
        {
           return System.Configuration.ConfigurationManager.AppSettings["SiteName"];

        }

        public static string PropertyImageFolderPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["PropertyImageFolderPath"];
        }

        public static string TempImageFolderPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["TempImageFolderPath"];
        }

    }
}