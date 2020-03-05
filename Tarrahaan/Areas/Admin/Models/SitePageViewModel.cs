using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tarrahaan.Areas.Admin.Models
{
    public class SitePageViewModel
    {
        public int PageId { set; get; }

        public string LangId { set; get; }

        public string PageTitle { set; get; }

        [AllowHtml]
        public string PageContent { set; get; }
    }
}