using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace Tarrahaan.CoreLib.Localization
{
    public enum Lang
    {
        English = 0,
        Farsi =1
    }
    public static class Language
    {
        public static string GetCurrent()
        {
            return CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToString();
        }

        public static Lang GetLanguage()
        {
            switch (GetCurrent().ToLower())
            {
                case ("en"):
                    return Lang.English;
                case ("fa"):
                    return Lang.Farsi;
                default:
                    return Lang.English; //User Request to change language by URL
            }
        }

        public static void ChangeCulture(string lang)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
           // Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        }

        public static void ChangeCulture(Lang lang)
        {
            switch (lang)
            {
                case Lang.English:
                    ChangeCulture("en");
                    break;
                case Lang.Farsi:
                    ChangeCulture("fa");
                    break;
                default:
                    ChangeCulture("en");
                    break;
                    // throw new ArgumentOutOfRangeException(nameof(lang), lang, null);
            }
        }
    }
}