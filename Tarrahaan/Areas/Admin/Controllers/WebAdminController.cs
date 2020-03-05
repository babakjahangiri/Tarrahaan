using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.CoreLib.Localization;

namespace Tarrahaan.Areas.Admin.Controllers
{
   // [AuthorizeByLocalization]
    public class WebAdminController : Controller
    {
        
        public void Index()
        {
            Response.Redirect("~/WebAdmin/en/DashBoard");
        }
    }
}