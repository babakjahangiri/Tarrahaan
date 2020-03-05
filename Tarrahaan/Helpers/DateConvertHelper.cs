using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.CoreLib.Localization;
using Tarrahaan.CoreLib.WebSite;

namespace Tarrahaan.Helpers
{
    public static class DateConvertHelper
    {
        public static MvcHtmlString ConvertToCultureDate(this HtmlHelper htmlHelper, DateTime inputDate)
        {
            var outputDate = "";

            switch (CoreLib.Localization.Language.GetLanguage())
            {
                 case Lang.Farsi:
                    outputDate= ConvertToJalali(inputDate);
                    break;

                case Lang.English:
                    outputDate= ConvertToGregorian(inputDate);
                    break;

                default:
                    outputDate = ConvertToGregorian(inputDate);
                    break;
            }

            return MvcHtmlString.Create(outputDate);
        }

        private static string ConvertToJalali(DateTime inputDate)
        {
             var pcal = new PersianCalendar();
             var year = pcal.GetYear(inputDate).ToString("0000");
             var month = (pcal.GetMonth(inputDate).ToString("00"));
             var day = (pcal.GetDayOfMonth(inputDate).ToString("00"));

            return year + "/" + month + "/" + day;
        }


        private static string ConvertToGregorian(DateTime inputDate)
        {
           return inputDate.ToLongDateString();
        }
    }
}