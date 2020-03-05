using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using Tarrahaan.CoreLib.Localization;
using Tarrahaan.Models;
using Tarrahaan.Models.SiteModels;
using Tarrahaan.ServiceLayer;

namespace Tarrahaan.Controllers
{
    public class MainController : Controller
    {
        private readonly PropertyService _proservice;
        private readonly SitePageService _sitePageService;

        public MainController()
        {
            _proservice = new PropertyService();
            _sitePageService = new SitePageService();
        }
        // GET: Main
        [Localization]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Home()
        {
            var langId= Language.GetCurrent();
            ViewBag.Lang = langId;
          //  var listlatestproperties =_proservice.ListLatestProperties(langId);

          //  return View(listlatestproperties);

            return View();
        }

        // GET: Main
        [Localization]
        public ActionResult LatestProperties()
        {

            var langId = Language.GetCurrent();
            ViewBag.Lang = langId;
            var listlatestproperties = _proservice.ListLatestProperties(langId);

            return PartialView("_LatestProperties", listlatestproperties);
        }


        [Localization]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult About()
        {
            ModelState.Clear();

           var langId = Language.GetCurrent();

            ViewBag.lang = langId;

            IEnumerable<ShowSitePageViewModel> sitePageModel =
                _sitePageService.GetSitePageForUsers(1, langId);

            return View(sitePageModel);
        }


        [Localization]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Contact()
        {
            ModelState.Clear();

            var langId = Language.GetCurrent();

            IEnumerable<ShowSitePageViewModel> sitePageModel =
                _sitePageService.GetSitePageForUsers(2, langId);

            return View(sitePageModel);

        }

        //[Localization]
        //public ActionResult Test()
        //{
        //    ModelState.Clear();

        //    var langId = Language.GetCurrent();

        //    ViewBag.lang = langId;


        //    var email =
        //        new MailMessage(new MailAddress("smtp@tarahanagency.com", "(do not reply)"),
        //            new MailAddress("babakjahangiri@gmail.com"))
        //        {
        //            Subject = "test tarahan",
        //            Body = "1234",
        //            IsBodyHtml = true
        //        };



        //    using (var smtp = new SmtpClient())
        //    {
        //        // var smtpnetworksettings = new System.Net.Configuration.SmtpSection().Network;
        //        //var credential = new NetworkCredential
        //        //{
        //        //    UserName = smtpnetworksettings.UserName,
        //        //    Password = smtpsettings.Password,
        //        //};
        //        //   smtp.Credentials = credential;
        //        //  smtp.Host = smtpsettings.Host;
        //        //  smtp.Port = 587;
        //        //  smtp.EnableSsl = true;
        //        try
        //        {
        //            smtp.Send(email);
        //        }
        //        catch (Exception ex)
        //        {
                    
        //            throw new Exception(ex.Message);
        //        }
               



        //        return null;
        //    }

       // }
    }
}