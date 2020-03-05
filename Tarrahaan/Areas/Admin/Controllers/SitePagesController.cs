using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Tarrahaan.Areas.Admin.Models;
using Tarrahaan.CoreLib.Localization;
using Tarrahaan.ServiceLayer;

namespace Tarrahaan.Areas.Admin.Controllers
{
    [AuthorizeByLocalization(Roles = "Admin")]
    [RouteArea("Admin", AreaPrefix = "WebAdmin")]
    [RoutePrefix(@"{lang:regex([a-zA-Z]{2})}/SitePages")]
    [Localization]
    public class SitePagesController : Controller
    {
        private readonly SitePageService _sitePageService;

        public SitePagesController()
        {
            _sitePageService = new SitePageService();
        }

        // GET: WebAdmin/en/SitePages/ListPages
        [Route("List", Name = "ListSitePageRoute")]
        [HttpGet]
        public ActionResult List()
        {
            return View(_sitePageService.GetSitePageList());
        }

        // GET: WebAdmin/en/SitePages/Edit/1
        [Route("Edit/{SitePageId}", Name= "EditSitePageRoute")]
        [HttpGet]
        public ActionResult Edit(string pageLang)
        {
            //var langId = RouteData.Values["lang"].ToString(); 
            var pageId = Convert.ToInt32(RouteData.Values["SitePageId"].ToString());

             var model = _sitePageService.GetSitePageForAdmin(pageId, pageLang);
            return View(model);
        }


        [Route("Edit/{SitePageId}", Name = "EditSitePagePostRoute")]
        [HttpPost]
        public ActionResult Edit(SitePageViewModel sitePage, string pageLang)
        {
            //var langId = RouteData.Values["lang"].ToString(); 
            var pageId = Convert.ToInt32(RouteData.Values["SitePageId"].ToString());

            var updateOk = false;

            try
            {
                updateOk = _sitePageService.UpdateSitePageForAdmin(sitePage);
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            if (updateOk)
              //  HttpResponse.RemoveOutputCacheItem(Url.RouteUrl("MainRoute"));
            return RedirectToRoute("ListSitePageRoute");
          

          
          var model = _sitePageService.GetSitePageForAdmin(pageId, pageLang);
          return View(model);
          

           
        }
    }
}