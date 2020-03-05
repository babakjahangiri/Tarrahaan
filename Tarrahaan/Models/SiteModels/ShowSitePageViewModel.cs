using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.Models.SiteModels
{
    public class ShowSitePageViewModel
    {
        public int PageId { set; get; }

        public string LangId { set; get; }

        public string PageTitle { set; get; }

        public string PageContent { set; get; }

    }
}