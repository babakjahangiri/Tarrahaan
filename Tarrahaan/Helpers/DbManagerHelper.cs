using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls.Expressions;
using Glimpse.Core.Extensions;
using Microsoft.Ajax.Utilities;

namespace Tarrahaan.Helpers
{

    public static class DbManagerHelper
    {
        private static int TotalPageCount { get; set; }
        private static int CurrentPage { get; set; }
        private static int PageStep { get; set; }
        private static long TotalRecord { get; set; }
        private static int TotalSectionCount { get; set; }
        private static int PagesInNavigation { get; set; }

        public static IDictionary<string, object> MergeHtmlAttributes1(this HtmlHelper helper,
            object htmlAttributesObject, object defaultHtmlAttributesObject)
        {

            var concatKeys = new string[] {"class"};

            var htmlAttributesDict = htmlAttributesObject as IDictionary<string, object>;
            var defaultHtmlAttributesDict = defaultHtmlAttributesObject as IDictionary<string, object>;

            RouteValueDictionary htmlAttributes = (htmlAttributesDict != null)
                ? new RouteValueDictionary(htmlAttributesDict)
                : HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);

            RouteValueDictionary defaultHtmlAttributes = (defaultHtmlAttributesDict != null)
                ? new RouteValueDictionary(defaultHtmlAttributesDict)
                : HtmlHelper.AnonymousObjectToHtmlAttributes(defaultHtmlAttributesObject);

            foreach (var item in htmlAttributes)
            {
                if (concatKeys.Contains(item.Key))
                {
                    defaultHtmlAttributes[item.Key] = (defaultHtmlAttributes[item.Key] != null)
                        ? string.Format("{0} {1}", defaultHtmlAttributes[item.Key], item.Value)
                        : item.Value;
                }
                else
                {
                    defaultHtmlAttributes[item.Key] = item.Value;
                }
            }

            return defaultHtmlAttributes;
        }

        public static MvcHtmlString Pagination(this HtmlHelper htmlHelper, string name, int page, int pageSize,
            long totalRecord,
            string urlPath,object htmlAttributes = null)
        {

            PagesInNavigation = 5;
            PageStep = pageSize; // page increasing by one or more
            TotalRecord = totalRecord;
            CurrentPage = page;

            var htmlString = "";


            var tb = new TagBuilder("ul");
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tb.MergeAttribute("id", name);
            tb.MergeAttribute("name", name);
            htmlString = tb.ToString(TagRenderMode.StartTag); //wow !!!
            htmlString = htmlString + MakeNavigation(urlPath) + "</ul>";
            //-----------------------------------------

            return MvcHtmlString.Create(htmlString);
        }


        private static string MakeNavigation(string urlPath)
        {

            var pagingTag = "";
            var url = Addkey(urlPath);

            TotalPageCount = (int) (TotalRecord/PageStep);
            if ((TotalRecord%PageStep) > 0)
                TotalPageCount++;


            TotalSectionCount = (int) (TotalPageCount/PagesInNavigation);
            if ((TotalPageCount%PagesInNavigation) > 0)
                TotalSectionCount++;

            int firstPageNoInSection = GetFirstPageOfSection(CurrentPage);


            if (GetSectionByPageNumber(CurrentPage) == 1)
                pagingTag = pagingTag +
                            "<li class=\"disabled\"><a href=\"javascript:void(0)\"><span aria-hidden=\"true\">&laquo;</span></a></li>";
            else
                pagingTag = pagingTag + "<li><a href=\"" + url + (GetFirstPageOfSection(CurrentPage) - 1) +
                            "\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>";


            for (int i = firstPageNoInSection; i <= ((firstPageNoInSection + PagesInNavigation) - 1); i++)
            {
                if (i <= TotalPageCount)
                {
                    if (CurrentPage == i)
                    {
                        pagingTag = pagingTag + "<li class=\"active\"><a href=\"javascript:void(0)\">" + i.ToString() +
                                    "</a></li>";
                    }
                    else
                        pagingTag = pagingTag + "<li><a href=\"" + url + i.ToString() + "\">" + i.ToString() +
                                    "</a></li>";
                }
            }


            if (GetSectionByPageNumber(CurrentPage) == TotalSectionCount)
                pagingTag = pagingTag +
                            "<li class=\"disabled\"><a href=\"javascript:void(0)\"><span aria-hidden=\"true\">&raquo;</span></a></li>";
            else
                pagingTag = pagingTag + "<li><a href=\"" + url +
                            (GetFirstPageOfSection(CurrentPage) + PagesInNavigation) +
                            "\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a></li>";


            return pagingTag;
        }


        private static int GetFirstPageOfSection(int pageNum)
        {
            int sectionNo = GetSectionByPageNumber(pageNum);
            return ((sectionNo - 1)*PagesInNavigation) + 1;
        }


        private static int GetSectionByPageNumber(int pageNum)
        {
            int currentSection = (int) (pageNum/PagesInNavigation);
            if ((pageNum%PagesInNavigation) > 0)
                currentSection++;

            return currentSection;

        }


        private static string Addkey(string urlPath)
        {
            var lastchar = urlPath.Substring(urlPath.Length - 1);

            // if true means there is Question Mark(?) in URL
            var hasQm = (urlPath.IndexOf("?", StringComparison.Ordinal) != -1);

            if (hasQm)
            {
                switch (lastchar)
                {
                    case "?":
                        return urlPath + "page=";
                    default:
                        return urlPath + "&page=";
                }
            }
            else
                return urlPath + "?page=";

        }
    }
}
