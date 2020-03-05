using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.CoreLib.Localization;

namespace Tarrahaan.Controllers
{
    public class HomeController : Controller
    {
        [Localization]
        public ActionResult Index()
        {
            return View();
        }

    }
}