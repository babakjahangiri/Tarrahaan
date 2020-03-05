using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic;
using Tarrahaan.CoreLib.Localization;
using Tarrahaan.Areas.Admin.Models;
using Tarrahaan.CoreLib.ImageManager;
using Tarrahaan.CoreLib.WebSite;
using Tarrahaan.DAL.Entities;
using Tarrahaan.ServiceLayer;

namespace Tarrahaan.Areas.Admin.Controllers
{
    [AuthorizeByLocalization(Roles = "Admin")]
    [RouteArea("Admin", AreaPrefix = "WebAdmin")]
    [RoutePrefix(@"{lang:regex([a-zA-Z]{2})}/Property")]
    [Localization]
    public class PropertyController : Controller
    {
        private readonly PropertyService _proservice;
        private readonly FormObjectService _formObjectService;


        public PropertyController()
        {
            _proservice = new PropertyService();
            _formObjectService = new FormObjectService();

        }

        // GET: WebAdmin/en/Property/Add
        [Route("Add", Name="AddPropertyAdminRoute")]
        [HttpGet]
        public ActionResult Add()
        {

            ModelState.Clear();
            // --------- Load Default Category Data ----------------
            var langId =RouteData.Values["lang"].ToString();
            byte categoryId = 1;

            // --------------------- Fill Form Data ----------------------------------

            //var propertyContentList = new List<PropertyLangMapping>();
            //for (var i = 0; i <2; i++)
            //{
            //    propertyContentList.Add(new PropertyLangMapping());
            //   // sampleList[i].Title = "my title";
            //   // sampleList[i].Details = "my detials";
            //}


            var propertyViewModel = new PropertyViewModel
            {
                ItemCode = "",
                CategoryList = _formObjectService.DropDwonCategory(langId, 0),
                TypeList = _formObjectService.DropDwonType(langId, (byte)categoryId, 0),
                ActionList = _formObjectService.ListAction(langId,1),
                CurrencyList = _formObjectService.DropDownCurrency(1),
                BedRoomNo = 0,
                BathRoomNo = 0,
                ParkingNo = 0,
                FurnishingList = _formObjectService.DropDwonFurnishing(langId, 0),
                Builtup = 0,
                LandArea = 0,
                RentDepositPrice = 0.ToString(),
                RentMonthlyPrice = 0.ToString(), 
                AskingPrice = 0.ToString(),
                AdminComment = "",
                Visible = true
                //  PropertyLangMapping = propertyContentList
            };

             return View(propertyViewModel);
        }

        // POST: WebAdmin/en/Property/Add
        [Route("Add", Name = "AddPropertyAdminPostRoute")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PropertyViewModel propertyViewModel)
        {
           // ModelState.Clear();

            long propertyId = 0;

           try
           {
               propertyId = _proservice.InsertProperty(propertyViewModel);
           }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
          
            if (propertyId > 0)
                return RedirectToAction("Show",new { PropertyId = propertyId });

            //Load Property Form object if an Error happend 
            var langId = RouteData.Values["lang"].ToString();
            const byte categoryId = 1;

            propertyViewModel.CategoryList = _formObjectService.DropDwonCategory(langId, 0);
            propertyViewModel.TypeList = _formObjectService.DropDwonType(langId, (byte) categoryId, 0);
            propertyViewModel.ActionList = _formObjectService.ListAction(langId, 1);
            propertyViewModel.FurnishingList = _formObjectService.DropDwonFurnishing(langId, 0);
            propertyViewModel.CurrencyList = _formObjectService.DropDownCurrency(1);
            propertyViewModel.Visible = true;


            return View(propertyViewModel);
          
        }

        // GET: WebAdmin/en/Property/Show/4
        [Route("Show/{PropertyId:long}", Name = "ShowPropertyAdminRoute")]
        [HttpGet]
        public ActionResult Show()
        {
           var langId = RouteData.Values["lang"].ToString();
           var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
           var model = _proservice.ShowPropertyAdmin(propertyid, langId);
            ViewBag.PropertyId = propertyid;
            return View(model);
        }

        // GET: WebAdmin/en/Property/List/1
        [Route("List/{actionId}", Name = "ListAdminPropertiesbyActionRoute")]
        [Route("List", Name = "ListAdminPropertiesRoute")]
        [HttpGet]
        public ActionResult List(byte? actionId , int? page)
        {
            if ((page <= 0) || (page == null))
                page = 1;

            ModelState.Clear();

            var langId = RouteData.Values["lang"].ToString();

            const int pageSize = 5;

            ViewBag.PageSize = pageSize;
            ViewBag.PageNo = page;
            ViewBag.Lang = langId;

            long totalRecords;
            var model = _proservice.GetPropertyListAdmin(actionId.GetValueOrDefault(), langId,pageSize, page.GetValueOrDefault(), out totalRecords);

            // ---- For Paging ---------------
            ViewBag.TotalRecords = totalRecords;
            ViewBag.PagingPath = Url.RouteUrl("ListAdminPropertiesRoute", new { controller = "Property", action = "List", lang = @langId, actionId = @actionId });

            return View(model);

        }

        // Post: WebAdmin/en/Property/List
        [Route("List",Name="PropertyListAdminRoute")]
        [HttpPost]
        public ActionResult List(PropertyListPageViewModel propertylist)
        {
            //  ModelState.Clear();
            var langId = RouteData.Values["lang"].ToString();
            var actionId = propertylist.SelectedActionId;

            return RedirectToRoute("ListAdminPropertiesbyActionRoute",
                new {controller = "Property", action = "List", lang = @langId, actionId = @actionId});

        }

        [AllowAnonymous]
        [Route("GetPropertyType")]
        [HttpGet]
        public ActionResult GetPropertyType(int categoryId = 0)
        {
            var propertyTypes = _formObjectService.DropDwonType("en", (byte)categoryId,0);
            return Json(propertyTypes, JsonRequestBehavior.AllowGet);
        }

        // GET: WebAdmin/en/Property/Edit
        [Route("Edit/{PropertyId:long}", Name = "EditPropertyRoute", Order = 1)]
        [HttpGet]
        public ActionResult Edit()
        {
            ModelState.Clear();

            // --------- Load Property and It's Data ----------------
            var langId = RouteData.Values["lang"].ToString();
            var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
            var model = _proservice.ShowPropertyAdmin(propertyid, langId);

            ViewBag.PropertyId = propertyid;
            
            model.CategoryList = _formObjectService.DropDwonCategory(langId,model.SelectedCategoryId);
            model.TypeList = _formObjectService.DropDwonType(langId, (byte) model.SelectedCategoryId,model.SelectedTypeId);
            model.ActionList = _formObjectService.ListAction(langId, model.SelectedActionId);
            model.CurrencyList = _formObjectService.DropDownCurrency(model.SelectedCurrencyId);
            model.FurnishingList = _formObjectService.DropDwonFurnishing(langId, model.SelectedFurnishingId);

            return View(model);
        }

        // POST: WebAdmin/en/Property/Edit
        [Route("Edit/{PropertyId:long}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyViewModel propertyViewModel)
        {
            var editOk = false;

            try
            {
                editOk = _proservice.EditProperty(propertyViewModel);
               
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
            }

            if (editOk)
                return RedirectToRoute("ShowPropertyAdminRoute", new { propertyid = propertyViewModel.PropertyId });

            if (ViewBag.Error == null)
            {
                ViewBag.Error = "No record has updated !";
            }
            // --------- Load Property and It's Data ----------------
            var langId = RouteData.Values["lang"].ToString();
            var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
            var model = propertyViewModel; 

            model.CategoryList = _formObjectService.DropDwonCategory(langId, model.SelectedCategoryId);
            model.TypeList = _formObjectService.DropDwonType(langId, (byte)model.SelectedCategoryId, model.SelectedTypeId);
            model.ActionList = _formObjectService.ListAction(langId, model.SelectedActionId);
            model.CurrencyList = _formObjectService.DropDownCurrency(model.SelectedCurrencyId);
            model.FurnishingList = _formObjectService.DropDwonFurnishing(langId, model.SelectedFurnishingId);

            return View(propertyViewModel);
          
        }


        // GET: WebAdmin/en/Property/Delete/4
        [Route("Delete/{PropertyId:long}", Name = "DeletePropertyRoute")]
        [HttpGet]
        public ActionResult Delete()
        {
            var langId = RouteData.Values["lang"].ToString();
            var propertyId = Convert.ToInt64(RouteData.Values["propertyid"].ToString());

            try
            {
                _proservice.DeleteProperty(propertyId, HttpContext.ApplicationInstance.Context);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

             return RedirectToRoute("PropertyListAdminRoute");
        }


        // GET: WebAdmin/en/Property/Album/9
        [Route("Album/{PropertyId:long}", Name = "PropertyAlbumAdminRoute")]
        [HttpGet]
        public ActionResult Album()
        {
            var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
            var model = _proservice.GetPropertyAlbum(propertyid);
            ViewBag.PropertyId = propertyid;

            return View(model);
        }


        // Post: WebAdmin/en/Property/AlbumCropImage/9     //First
        [Route("AlbumCropImage/{PropertyId:long}", Name = "AlbumCropImagePostAdminRoute", Order = 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumCropImage(HttpPostedFileBase imageFile)
        {

            //Check File format And Dimension
            //NoT available 


            ///////////////////////////////////////////
            var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString()); //Get Property Id From Route Value
            var imageId = Guid.NewGuid();
            var tempFileName = imageId.ToString();

            if (imageFile == null)
            {
                return RedirectToRoute("PropertyAlbumAdminRoute", new {PropertyId = @propertyid});
            }
            var fileName = Path.GetFileName(imageFile.FileName) ?? "";
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(imageFile.FileName) ?? "";
            var newFileName = fileName.Replace(fileNameWithoutExt, tempFileName);
            var path = System.IO.Path.Combine(Server.MapPath("~/Content/TempImages"), newFileName);
               
            // file is uploaded
            try
            {
                imageFile.SaveAs(path);
            }
            catch (IOException ioexception)
            {
                throw new IOException(ioexception.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            // save the image path to the database or you can send image 
            // directly to database
            // in-case if you want to store byte[] ie. for DB
            // using (var memoryStream = new MemoryStream())
            // {
            //     file.InputStream.CopyTo(memoryStream);
            //     var byteArray = memoryStream.GetBuffer();
            // }


            // after successfully uploading redirect to the AlbumCropImage Get
            return RedirectToRoute("AlbumCropImageAdminRoute", new { PropertyId = @propertyid, ImageId = @imageId });
        }

        // GET: WebAdmin/en/Property/AlbumUpload/9  //Second
        [Route("AlbumCropImage/{PropertyId:long}/{ImageId:guid}", Name = "AlbumCropImageAdminRoute")]
        [HttpGet]
        public ActionResult AlbumCropImage()
        {

            var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
            var tempImageId = new Guid(RouteData.Values["ImageId"].ToString()); //Get Temp Image Id From Route Value

            //Get True Heigh and True Width of Image and Pass it ViewBag Therefore It fills JCrop True Size
            var imageFilePath = Server.MapPath(SiteInfo.TempImageFolderPath()) + "\\" + tempImageId + ".jpg";

            var imageTools = new ImageTools();
            var img = imageTools.MakeImage(imageFilePath);

            ViewBag.TrueW = img.Width.ToString();
            ViewBag.TrueH = img.Height.ToString();
            //-----------------------------------------------------

          //  ViewBag.test = Server.MapPath(SiteInfo.PropertyImageFolderPath()) + "\\" + propertyid + "\\large\\" + tempImageId + ".jpg";

            var model = new CropImageViewModel
            {
                PropertyId = propertyid,
                ImageId = tempImageId
            };

            return View(model);
        }


        // Post: WebAdmin/en/Property/AlbumSaveImage  
        [Route("AlbumSaveImage", Name = "AlbumSaveImagePostAdminRoute")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumSaveImage(CropImageViewModel model)
        {
            try
            {
                _proservice.InsertImage(model.PropertyId, model.ImageId, (int)model.Height, (int)model.Width, model.CoordX, model.CoordY, HttpContext.ApplicationInstance.Context);

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
          
            //  var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
            //  var tempImageId = new Guid(RouteData.Values["ImageId"].ToString()); //Get Temp Image Id From Route Value


            return RedirectToRoute("PropertyAlbumAdminRoute", new {PropertyId = @model.PropertyId});
        }

        

        // GET: WebAdmin/en/Property/ImageSetDefault/9/c9d378a7-37da-4065-bbc1-b58877d75ff5
        [Route("ImageSetDefault/{PropertyId:long}/{ImageId:guid}", Name = "ImageSetDefaultAdminRoute", Order = 1)]
        [HttpGet]
        public ActionResult ImageSetDefault()
        {
            var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
            Guid imageId;
            Guid.TryParse(RouteData.Values["ImageId"].ToString(),out imageId);
            
            _proservice.SetDefaultImage(propertyid,imageId);

           return RedirectToRoute("PropertyAlbumAdminRoute",new { PropertyId = @propertyid });

        }

        // GET: WebAdmin/en/Property/DeleteAlbumImage/9/c9d378a7-37da-4065-bbc1-b58877d75ff5
        [Route("DeleteAlbumImage/{PropertyId:long}/{ImageId:guid}", Name = "DeleteAlbumImageAdminRoute", Order = 1)]
        [HttpGet]
        public ActionResult DeleteAlbumImage()
        {
            var propertyid = Convert.ToInt64(RouteData.Values["PropertyId"].ToString());
            Guid imageId;
            Guid.TryParse(RouteData.Values["ImageId"].ToString(), out imageId);

            _proservice.DeleteImage(propertyid,imageId, HttpContext.ApplicationInstance.Context);

            return RedirectToRoute("PropertyAlbumAdminRoute", new { PropertyId = @propertyid });

        }

    }
}