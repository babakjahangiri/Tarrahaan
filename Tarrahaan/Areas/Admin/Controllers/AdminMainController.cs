using System;
using System.Web.Mvc;
using Tarrahaan.Areas.Admin.Models;
using Tarrahaan.CoreLib.Localization;
using Tarrahaan.ServiceLayer;

namespace Tarrahaan.Areas.Admin.Controllers
{
    [AuthorizeByLocalization(Roles ="Admin")]
    [RouteArea("Admin", AreaPrefix = "WebAdmin")]
    [RoutePrefix(@"{lang:regex([a-zA-Z]{2})}")]
    [Localization]
    public class AdminMainController : Controller
    {

        private readonly PropertyService _proservice;

        public AdminMainController()
        {
            _proservice = new PropertyService();
        }


        // GET: WebAdmin/en/DashBoard
        [Route("DashBoard",Name="AdminDashBoardRoute")]
        public ActionResult DashBoard()
        {
            var model = new DashBoardViewModel
            {
                TotalCount = _proservice.GetPropertyCount(PropertyActionType.All),
                SaleCount = _proservice.GetPropertyCount(PropertyActionType.Sale),
                RentCount = _proservice.GetPropertyCount(PropertyActionType.Rent)
            };

         
            return View(model);
        }

        // GET: WebAdmin/en/LogOffConfirm
        [Route("LogOffConfirm", Name = "LogOffConfirmRoute")]
        public ActionResult LogOffConfirm()
        {
            return View();
        }


        // GET: WebAdmin/en/LoggedOff
        [AllowAnonymous]
        [Route("LoggedOff", Name = "LoggedOffRoute")]
        public ActionResult LoggedOff()
        {
            return View("LoggedOff");
        }


    }
}