using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.CoreLib.WebSite;

namespace Tarrahaan.Helpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString GetPropertyImageLarge(this HtmlHelper htmlHelper,long propertyId, Guid imageId)
        {
            var imgPath = SiteInfo.PropertyImageFolderPath() + "/" + propertyId + "/large/" + imageId.ToString() + ".jpg";
            return MvcHtmlString.Create(imgPath);
        }

        public static MvcHtmlString GetPropertyImageThumbnail(this HtmlHelper htmlHelper,long propertyId, Guid imageId)
        {
            var imgPath = SiteInfo.PropertyImageFolderPath() + "/" + propertyId + "/thumb/t_" + imageId.ToString() + ".jpg";
            return MvcHtmlString.Create(imgPath);

        }

        public static MvcHtmlString GetPropertyImageTemp(this HtmlHelper htmlHelper,Guid imageId)
        {
            var imgPath = SiteInfo.TempImageFolderPath() + "/" + imageId.ToString() + ".jpg";
            return MvcHtmlString.Create(imgPath);

        }
    }
}