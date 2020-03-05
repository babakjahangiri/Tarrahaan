using System.Web;
using System.Web.Mvc;
using Tarrahaan.CoreLib.Localization;

namespace Tarrahaan
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationAttribute("en"),0);
            
        }
    }

}
