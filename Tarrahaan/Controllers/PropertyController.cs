using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls.Expressions;
using Tarrahaan.CoreLib.Localization;
using Tarrahaan.Models;
using Tarrahaan.ServiceLayer;
using Microsoft.VisualBasic;
using Tarrahaan.Models.SiteModels;
 

namespace Tarrahaan.Controllers
{
    public class PropertyController : Controller
    {
         private readonly PropertyService _proservice;
         private readonly FormObjectService _formObjectService;
         private readonly SeoService _seoService;


        public PropertyController()
        {
            _proservice = new PropertyService();
            _formObjectService = new FormObjectService();
            _seoService = new SeoService();

        }
        // GET: Property
        [HttpGet]
        [Localization]
        public ActionResult ListProperties(string lang,string praction, string category,string type,int page)
        {
            ModelState.Clear();

            // ---------- Check all input variables ----------------------------

            //Control the page value  , by Default its 1 , Set in Route Config 
            var strPage = Request["page"];
            if (Information.IsNumeric(strPage))    page = Convert.ToInt32(strPage);
            if (page < 1) page = 1;
       

            var strFurnishingId = Request.QueryString["furnish"];
            // ReSharper disable once NotAccessedVariable
            byte furnishingId = 0;
             

            if (Information.IsNumeric(strFurnishingId))   byte.TryParse(strFurnishingId, out furnishingId);
 
 
            var typeId = _proservice.GetTypeId(type);
            if (typeId == -1)
                return HttpNotFound("Property Type is not valid");
            

            var actionId = _proservice.GetActionId(praction);
           if (actionId == -1)
            {
            // return new  HttpStatusCodeResult(HttpStatusCode.NotFound);
             return HttpNotFound("This path is not valid.You Can Request for Sell or Rent ...");
            }

           var categoryId = _proservice.GetCategoryId(category);
            if (categoryId == -1)
            {
                // return new  HttpStatusCodeResult(HttpStatusCode.NotFound);
                return HttpNotFound("This path is not valid Category Name");
            }

            // --------------------------------------------------------------------

            ViewBag.CategoryList = _formObjectService.DropDwonCategory(lang, (byte)categoryId);
            ViewBag.TypeList = _formObjectService.DropDwonType(lang, (byte)categoryId, typeId);
            ViewBag.FurnishingList = _formObjectService.DropDwonFurnishing(lang, furnishingId);

             long totalRecords;
            //------------- Fill Model -----------------------------------
            var listproperties =
                _proservice.ListPropertiesSite(lang, (byte)actionId, categoryId, typeId, furnishingId, 5, (int)page , out totalRecords);

            // --------------------- FOR SEO --------------------------------------------------------

            ViewBag.Lang = lang;
            ViewBag.prAction = praction;
            ViewBag.Category = category;
            ViewBag.PageNo = page;
            //  ViewBag.PagingPath = ListPropertiesUrlPath(lang, praction, category, type,furnishingId.ToString());
            ViewBag.PagingPath = Url.RouteUrl(new
            {
                controller = "Property",
                action = "ListProperties",
                lang = @lang,
                praction = @praction,
                type = @type,
                category = @category,
                furnish = @furnishingId
            });

            ViewBag.TotalRecords = totalRecords;

            ViewBag.canonical = CoreLib.WebSite.SiteInfo.SiteName() + Url.RouteUrl(new { controller = "Property", action = "ListProperties", lang = @lang, praction = @praction});

            var categoryLocalName = _seoService.GetTypeCategoryLocalName(lang, (byte)categoryId);
            var propertyTypeLocalName = _seoService.GetTypeLocalName(lang, typeId);
            var propertyActionLocalName = _seoService.GetPropertyActionLocalName(lang, (byte) actionId);

            ViewBag.ActionLocalName = propertyActionLocalName;

            ViewBag.SeoTitle = Resources.Global.SeoTitle_ListProperties + " - " + categoryLocalName + ' ' + propertyTypeLocalName + ' ' +
                               propertyActionLocalName + " | " + Resources.Global.page + ' ' + page + ' ' +  Resources.Global.of + ' ' + totalRecords;
           
            // -----------------------------------------------------------------------------------------

            //Check For Page Empty by increasing page no 
            if ((!listproperties.Any()) && (page > 1))
            {
                return HttpNotFound();
            }

           return View(listproperties);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Localization]
        public ActionResult ListProperties(FormCollection form)
        {
            var strLang = Request["lang"];
            var strPrAction = Request["praction"];
            var strdrdCategoryListId = Request["CategoryList"];
            var strdrdTypeListId = Request["TypeList"];
            var strdrdFurnishingList = Request["FurnishingList"];
            var strPageNo = Request["page"];

            var pageNo = 1;
            if (Information.IsNumeric(strPageNo))
                pageNo = Convert.ToInt32(strPageNo);


            byte categoryId = 0;
            if (Information.IsNumeric(strdrdCategoryListId))
                categoryId = Convert.ToByte(strdrdCategoryListId);
            // var strCategoryId = _proservice.GetCategoryId(drdCategoryListId);
                var strCategoryName = _proservice.GetCategoryRouteName(categoryId);

            var typeId = 0;
            var strTypeName = "";
            // ReSharper disable once InvertIf
            if (categoryId != 0)
            {
                if (Information.IsNumeric(strdrdTypeListId))
                    typeId = Convert.ToInt32(strdrdTypeListId);
                  strTypeName = _proservice.GetTypeRouteName(typeId);
            }

            
            // ReSharper disable once NotAccessedVariable
            byte furnishingId = 0;

            if (Information.IsNumeric(strdrdFurnishingList))
                furnishingId = Convert.ToByte(strdrdFurnishingList);

            return
                RedirectToRoute(
                    new
                    {
                        controller = "Property",
                        action = "ListProperties",
                        lang = @strLang,
                        praction = @strPrAction,
                        type = @strTypeName,
                        category = @strCategoryName,
                        furnish = @furnishingId,
                        page = @pageNo
                    });
        }
      
        [OutputCache(Duration = 10, VaryByParam = "proId", Location=OutputCacheLocation.Server, NoStore=true)]
        [Localization]
        public ActionResult Show(string lang, long proId, string seoKeyword)
        {
            ModelState.Clear();

            var showPropertySiteModel = _proservice.ShowPropertySite(proId,lang);

             
            var propertyTitle = "";

            foreach (var item in showPropertySiteModel)
            {
                propertyTitle = item.Title; //Get Tilte For Seo Check
            }


          var showPropertyRoute = Url.RouteUrl("ShowProperty", new
          {
            lang = lang,
            controller = "Property",
            action = "Show",
            proId = proId,
            seoKeyword = CoreLib.WebSite.FriendlyUrl.Make(propertyTitle)
         });

            //If System Made Seo keyword Not Equal to Get SeoKeyword by Action 
            //Parmenet move to new Location and get correct canonical path
            
            if (!string.Equals(CoreLib.WebSite.FriendlyUrl.Make(propertyTitle), seoKeyword, StringComparison.CurrentCultureIgnoreCase))
            {
                return RedirectPermanent(showPropertyRoute);
            }

            ViewBag.Title = propertyTitle;
            ViewBag.canonical = CoreLib.WebSite.SiteInfo.SiteName() + showPropertyRoute;
            //if seoKeyword != title 304 parmenetly moved => orginal URL 

            // Check the seo keyword validation

            return View(showPropertySiteModel);
        }


    }
}