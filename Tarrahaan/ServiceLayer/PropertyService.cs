using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.VisualBasic;
using Tarrahaan.Areas.Admin.Models;
using Tarrahaan.CoreLib.ImageManager;
using Tarrahaan.DAL.Repository;
using Tarrahaan.Models;
using Tarrahaan.Models.SiteModels;
using Tarrahaan.CoreLib.Strings;
using Tarrahaan.CoreLib.WebSite;

namespace Tarrahaan.ServiceLayer
{
    public enum PropertyActionType
    {
        All = 0,
        Sale = 1,
        Rent = 2
    }

    public class PropertyService
    {
        private readonly IPropertyRepository _repoProperty;
        private readonly IPropertyLangMappingRepository _repoPropertyLangMapping;
        private readonly IPropertyActionRepository _repoPropertyAction;
        private readonly ITypeCategoryRepository _repoTypeCategory;
        private readonly IPropertyTypeRepository _repoPropertyType;
        private readonly IPropertyImagesRepository _repoPropertyImages;
        private readonly IPropertyTypeLangMappingRepository _repoPropertyTypeLangMapping;
        private readonly IPropertyFurnishingLangMappingRepository _repoPropertyFurnishingLangMapping;
        private readonly ITypeCategoryLangMappingRepository _repoTypeCategoryLangMappingRepository;
        private readonly IPropertyActionLangMappingRepository _repoPropertyActionLangMapping;
        private readonly ICurrencyRepository _repoCurrency;

        public PropertyService()
        {
            _repoProperty = new PropertyRepository();
            _repoPropertyLangMapping = new PropertyLangMappingRepository();
            _repoPropertyAction = new PropertyActionRepository();
            _repoTypeCategory = new TypeCategoryRepository();
            _repoPropertyType = new PropertyTypeRepository();
            _repoPropertyImages = new PropertyImagesRepository();
            _repoPropertyTypeLangMapping = new PropertyTypeLangMappingRepository();
            _repoPropertyFurnishingLangMapping = new PropertyFurnishingLangMappingRepository();
            _repoTypeCategoryLangMappingRepository = new TypeCategoryLangMappingRepository();
            _repoPropertyActionLangMapping = new PropertyActionLangMappingRepository();
            _repoCurrency = new CurrencyRepository();


        }

        internal int GetActionId(string actionName)
        {
            var actionId = _repoPropertyAction.GetId(actionName);
            if ((actionName.ToLower() == "") || (actionId == 0))
            {
                return -1; //which means wrong property action Name or empty property action 
            }

            return actionId;
        }

        internal int GetTypeId(string typeName)
        {
            var typeId = _repoPropertyType.GetId(typeName);
            if ((typeName.ToLower() != "") && (typeId == 0))
            {
                return -1; //which means wrong property action Name or empty property action 
            }

            return typeId;
        }

        internal string GetTypeRouteName(int typeId)
        {
            return _repoPropertyType.GetName(typeId); //Return Type General Name
        }

        internal string GetCategoryRouteName(byte categoryId)
        {
            return _repoTypeCategory.GetName(categoryId); //Return Category General Name
        }

        internal int GetCategoryId(string categoryName)
        {

            var categoryId = _repoTypeCategory.GetId(categoryName);

            if ((categoryName.ToLower() != "") && (categoryId == 0))
            {
                return -1; //which means wrong category Name 
            }

            return categoryId;
        }


        public IEnumerable<ListPropertiesSiteModels> ListPropertiesSite(string langId, byte actionId, int categoryId,
            int typeId, byte furnishingId, int pageSize, int pageNumber, out long count)
        {

            var dt = _repoProperty.ListPropertiesforUsers(langId, actionId, (byte) categoryId, typeId, furnishingId,
                pageSize, pageNumber, out count);

            // return AutoMapper.Mapper.DynamicMap<IDataReader, IEnumerable<Property>>(dt.CreateDataReader());

            var prolist = (from DataRow dr in dt.Rows

                select new ListPropertiesSiteModels()
                {
                    Id = Convert.ToInt64(dr["PropertyId"]),
                    Title = Convert.ToString(dr["Title"]),
                    Description = Convert.ToString(dr["Description"]),
                    RoomNo = Convert.ToByte(dr["Bedroom"]),
                    BathNo = Convert.ToByte(dr["Bathroom"]),
                    ParkingNo = Convert.ToByte(dr["Parking"]),
                    Builtup = (float) Convert.ToDouble(dr["Builtup"]),
                    ImageId = _repoPropertyImages.GetDefaultImageId(Convert.ToInt64(dr["PropertyId"])),
                    ImagesCount = _repoPropertyImages.GetImagesCount(Convert.ToInt64(dr["PropertyId"]))

                }).AsEnumerable();

            return prolist;
        }

        public IEnumerable<ListLatestPropertiesSiteModels> ListLatestProperties(string langId)
        {
            const int count = 4;

            var dt = _repoProperty.GetLatestProperties(langId, count);

            var prolist = (from DataRow dr in dt.Rows

                select new ListLatestPropertiesSiteModels()
                {
                    Id = Convert.ToInt64(dr["PropertyId"]),
                    Title = Convert.ToString(dr["Title"]),
                    ActionId = Convert.ToByte(dr["ActionId"]),
                    Action = Convert.ToString(dr["Action"]),
                    ImageId = _repoPropertyImages.GetDefaultImageId(Convert.ToInt64(dr["PropertyId"]))

                }).AsEnumerable();

            return prolist;
        }

        public IEnumerable<ShowPropertySiteModel> ShowPropertySite(long propertyId, string langId)
        {
            //** Just Simple map from DataReader :)) To ShowPropertySiteModel View Model **

            var dtProperty = _repoProperty.GetProperty(propertyId, langId);
            var dtImages = _repoPropertyImages.GetVisibleImages(propertyId);

            var property = (from DataRow dr in dtProperty.Rows
                select new ShowPropertySiteModel()
                {
                    PropertyId = Convert.ToInt64(dr["PropertyId"]),
                    ItemCode = Convert.ToString(dr["ItemCode"]),
                    Title = Convert.ToString(dr["Title"]),
                    Details = Convert.ToString(dr["Details"]),
                    BedRoomNo = Convert.ToByte(dr["Bedroom"]),
                    BathRoomNo = Convert.ToByte(dr["Bathroom"]),
                    ParkingNo = Convert.ToByte(dr["Parking"]),
                    Builtup = (float) Convert.ToDouble(dr["Builtup"]),
                    LandArea = (float) Convert.ToDouble(dr["LandArea"]),
                    Furnishing = Convert.ToString(dr["Furnishing"]),
                    ActionId = Convert.ToByte(dr["ActionId"]),
                    Action = Convert.ToString(dr["Action"]),
                    Category = Convert.ToString(dr["Category"]),
                    Type = Convert.ToString(dr["Type"]),
                    AskingPrice = Convert.ToInt64(dr["PriceTotal"]),
                    RentMonthlyPrice = Convert.ToInt64(dr["PriceRental"]),
                    RentDepositPrice = Convert.ToInt64(dr["PriceDeposite"]),
                    CurrencySign = Convert.ToString(dr["CurrencySign"]),
                    CurrencyIsoCode = Convert.ToString(dr["CurrencyIsoCode"]),
                    Visible = Convert.ToBoolean(dr["Visible"]),
                    LastUpdate = Convert.ToDateTime(dr["UpdateDate"]),
                    Images = (from DataRow dr1 in dtImages.Rows
                        select new ShowPropertyImagesViewModel()
                        {
                            ImageId = Guid.Parse(Convert.ToString(dr1["ImageId"])),
                            // IsDefault = Convert.ToBoolean(dr1["IsDefault"]),
                            PropertyId = Convert.ToInt64(dr1["PropertyId"]),
                            // Visible = Convert.ToBoolean(dr1["Visible"])

                        }).ToList()
                }).AsEnumerable();

            return property;
        }


        public PropertyViewModel ShowPropertyAdmin(long propertyId, string langId)
        {
            //---> its diffrent implementation , at the top i used IEnumerable but here class object
            // I Like This Way , My Favorite
            var propertyEntity = _repoProperty.Load(propertyId);
            var propertyLangMappingEntity = _repoPropertyLangMapping.List(propertyId);
            var propertyViewModel = new PropertyViewModel
            {
                PropertyId = propertyEntity.PropertyId,
                ItemCode = propertyEntity.ItemCode,
                SelectedCategoryId = propertyEntity.CategoryId,
                CategoryName = _repoTypeCategoryLangMappingRepository.GetName(langId, propertyEntity.CategoryId),
                SelectedTypeId = propertyEntity.TypeId,
                TypeName = _repoPropertyTypeLangMapping.GetName(langId, propertyEntity.TypeId),
                SelectedActionId = propertyEntity.ActionId,
                ActionName = _repoPropertyActionLangMapping.GetSellerActionName(langId, propertyEntity.ActionId),
                SelectedCurrencyId = propertyEntity.CurrencyId,
                CurrencyName = _repoCurrency.GetName(propertyEntity.CurrencyId),
                BedRoomNo = propertyEntity.Bedroom,
                BathRoomNo = propertyEntity.Bathroom,
                ParkingNo = propertyEntity.Parking,
                SelectedFurnishingId = propertyEntity.FurnishingId,
                FurnishingName = _repoPropertyFurnishingLangMapping.GetName(langId, propertyEntity.FurnishingId),
                Builtup = propertyEntity.Builtup,
                LandArea = propertyEntity.LandArea,
                RentDepositPrice = String.Format("{0:N0}", propertyEntity.PriceDeposite),
                RentMonthlyPrice = String.Format("{0:N0}", propertyEntity.PriceRental),
                AskingPrice = String.Format("{0:N0}", propertyEntity.PriceTotal),
                AdminComment = propertyEntity.AdminComment,
                PostedDate = propertyEntity.PostedDate,
                LastUpdate = propertyEntity.UpdateDate,
                Visible = propertyEntity.Visible,
                PropertyLangMapping = (from item in propertyLangMappingEntity
                    select new PropertyLangMappingViewModel()
                    {
                        PropertyId = item.PropertyId,
                        Title = item.Title,
                        Details = item.Details,
                        LangId = item.LangId,
                        Complete = item.Complete,
                        Visible = item.Visible
                    }).ToList()
            };

            return propertyViewModel;

        }

        public PropertyListPageViewModel GetPropertyListAdmin(byte actionId, string langId, int pageSize, int pageNumber,
            out long totalRecords)
        {

            var propertyEntity = _repoProperty.ListPropertiesforAdmin(actionId, pageSize, pageNumber, out totalRecords);

            var propertyListItems = (from item in propertyEntity
                select new PropertyListItem()
                {
                    Id = item.PropertyId,
                    ItemCode = item.ItemCode,
                    ImageId = _repoPropertyImages.GetDefaultImageId(item.PropertyId),
                    Title = _repoPropertyLangMapping.GetTitle(item.PropertyId, langId),
                    ActionName = _repoPropertyActionLangMapping.GetSellerActionName(langId, item.ActionId),
                    PostedDate = item.PostedDate
                });

            var listAction = new FormObjectService();

            var model = new PropertyListPageViewModel
            {
                SelectedActionId = actionId,
                ActionList = listAction.ListAction(langId, actionId),
                PropertyItem = propertyListItems
            };

            return model;

        }

        public PropertyAlbumViewModel GetPropertyAlbum(long propertyId)
        {
            var imageEntity = _repoPropertyImages.GetImages(propertyId);

            var albumImages = (from DataRow dr in imageEntity.Rows
                select new PropertyImageViewModel()
                {
                    PropertyId = Convert.ToInt64(dr["PropertyId"]),
                    ImageId = (Guid) dr["ImageId"],
                    Visible = Convert.ToBoolean(dr["Visible"]),
                    IsDefault = Convert.ToBoolean(dr["IsDefault"])

                }).ToList();

            var propertyAlbumModel = new PropertyAlbumViewModel
            {
                PropertyId = propertyId,
                AlbumImages = albumImages
            };

            return propertyAlbumModel;
        }

        public void SetDefaultImage(long propertyId, Guid imageId)
        {
            _repoPropertyImages.UnsetAllDefaultImage(propertyId);
            _repoPropertyImages.SetDefaultImage(propertyId, imageId);
        }


        public long GetPropertyCount(PropertyActionType action)
        {
            return _repoProperty.GetCount((byte) action);
        }

        public long InsertProperty(PropertyViewModel propery)
        {
            bool flag = false;

            //** Just Simple map from Property Entity To Property View Model **
            var propertyEntity = new Tarrahaan.DAL.Entities.Property
            {
                ItemCode = Strings.Trim(propery.ItemCode),
                CategoryId = propery.SelectedCategoryId,
                TypeId = propery.SelectedTypeId,
                ActionId = propery.SelectedActionId,
                Bathroom = propery.BathRoomNo,
                Bedroom = propery.BedRoomNo,
                Parking = propery.ParkingNo,
                Builtup = propery.Builtup,
                LandArea = propery.LandArea,
                FurnishingId = propery.SelectedFurnishingId,
                PriceRental = StringConvert.ToMoney(propery.RentMonthlyPrice),
                PriceDeposite = StringConvert.ToMoney(propery.RentDepositPrice),
                PriceTotal = StringConvert.ToMoney(propery.AskingPrice),
                CurrencyId = propery.SelectedCurrencyId,
                PostedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                AdminComment =
                    Strings.Trim((string.IsNullOrEmpty(propery.AdminComment)) ? string.Empty : propery.AdminComment),
                Visible = propery.Visible
            };

            long insertedPropertyId = 0;

            try
            {
                insertedPropertyId = _repoProperty.Insert(propertyEntity);
                if (insertedPropertyId > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Property Failed  : " + ex.Message);
            }

            if (flag != true) //If Still Flag = False ...
                return -1;

            var propertyLangMappingEntity = new Tarrahaan.DAL.Entities.PropertyLangMapping();

            foreach (var item in propery.PropertyLangMapping)
            {

                propertyLangMappingEntity.PropertyId = insertedPropertyId;
                propertyLangMappingEntity.LangId = item.LangId;
                propertyLangMappingEntity.Title = string.IsNullOrEmpty(item.Title) ? string.Empty : item.Title;
                propertyLangMappingEntity.Details = string.IsNullOrEmpty(item.Details) ? string.Empty : item.Details;
                //** If Title was Not Empty, Make Property Content for language  Visible **
                propertyLangMappingEntity.Complete = !string.IsNullOrEmpty(item.Title);
                propertyLangMappingEntity.Visible = !string.IsNullOrEmpty(item.Title);

                try
                {
                    var insertPropContent = _repoPropertyLangMapping.Insert(propertyLangMappingEntity);
                    if (insertPropContent == false)
                        return -1;
                }

                catch (Exception ex)
                {
                    throw new Exception("Insert Property Content Failed  : " + ex.Message);
                }
            }

            return insertedPropertyId;
        }

        public bool EditProperty(PropertyViewModel propery)
        {

            var propertyEntity = new Tarrahaan.DAL.Entities.Property
            {
                PropertyId = propery.PropertyId,
                ItemCode = Strings.Trim(propery.ItemCode),
                CategoryId = propery.SelectedCategoryId,
                TypeId = propery.SelectedTypeId,
                ActionId = propery.SelectedActionId,
                Bathroom = propery.BathRoomNo,
                Bedroom = propery.BedRoomNo,
                Parking = propery.ParkingNo,
                Builtup = propery.Builtup,
                LandArea = propery.LandArea,
                FurnishingId = propery.SelectedFurnishingId,
                PriceRental = StringConvert.ToMoney(propery.RentMonthlyPrice),
                PriceDeposite = StringConvert.ToMoney(propery.RentDepositPrice),
                PriceTotal = StringConvert.ToMoney(propery.AskingPrice),
                CurrencyId = propery.SelectedCurrencyId,
                PostedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                AdminComment =
                    Strings.Trim(string.IsNullOrEmpty(propery.AdminComment) ? string.Empty : propery.AdminComment),
                Visible = propery.Visible
            };

            try
            {
                if (_repoProperty.Update(propertyEntity) != true)
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            // ------------- Update LangMapping List --------------

            var propertyLangMappingEntity = new Tarrahaan.DAL.Entities.PropertyLangMapping();

            foreach (var item in propery.PropertyLangMapping)
            {

                propertyLangMappingEntity.PropertyId = propery.PropertyId;
                propertyLangMappingEntity.LangId = item.LangId;
                propertyLangMappingEntity.Title = string.IsNullOrEmpty(item.Title) ? string.Empty : item.Title;
                propertyLangMappingEntity.Details = string.IsNullOrEmpty(item.Details) ? string.Empty : item.Details;
                //** If Title was Not Empty, Make Property Content for language  Visible **
                propertyLangMappingEntity.Complete = !string.IsNullOrEmpty(item.Title);
                propertyLangMappingEntity.Visible = !string.IsNullOrEmpty(item.Title);


                try
                {
                    if (_repoPropertyLangMapping.Update(propertyLangMappingEntity) != true)
                        return false;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

            }

            return true;
        }

        public bool DeleteProperty(long propertyId, HttpContext ctx)
        {

            //first Delete All Images and Images in Entity Database ----

            try
            {
                _repoPropertyImages.Delete(propertyId);

                ImagePath.DeleteAll(ctx.Server.MapPath(SiteInfo.PropertyImageFolderPath()) + "\\" + propertyId);
            }
            catch (Exception ex)
            {
                throw new Exception("Services  > Delete Files :" + ex.Message);
            }

            try
            {
                if (_repoPropertyLangMapping.Delete(propertyId) != true)
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            try
            {
                if (_repoProperty.Delete(propertyId) != true)
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return true;

        }

        public void InsertImage(long propertyId, Guid imageId, int height, int width, double coordX, double coordY,
            HttpContext ctx)
        {

            var imageTempFilePath = ctx.Server.MapPath(SiteInfo.TempImageFolderPath()) + "\\" + imageId + ".jpg";

            var imageFolderThumb = ctx.Server.MapPath(SiteInfo.PropertyImageFolderPath()) + "\\" + propertyId +
                                   "\\thumb";
            var imageFolderLarge = ctx.Server.MapPath(SiteInfo.PropertyImageFolderPath()) + "\\" + propertyId +
                                   "\\large";

            ImagePath.ClearTempFolder(ctx.Server.MapPath(SiteInfo.TempImageFolderPath()));

            ImagePath.CreateFolder(imageFolderThumb);
            ImagePath.CreateFolder(imageFolderLarge);

            var imageThumbFilePath = imageFolderThumb + "\\t_" + imageId + ".jpg";
            var imageLargeFilePath = imageFolderLarge + "\\" + imageId + ".jpg";

            var imageTools = new ImageTools();
            var img = imageTools.MakeImage(imageTempFilePath);

            if (img == null) return;
            var userCroppedImage = imageTools.CropImage(img, height, width, (float) coordX, (float) coordY);


            var thumbImage = imageTools.ResizeImage(userCroppedImage, 200, 200);
            imageTools.CompressAndSave(thumbImage, imageThumbFilePath);

            var largeImage = imageTools.ResizeImage(userCroppedImage, 600, 600);
            imageTools.CompressAndSave(largeImage, imageLargeFilePath);

            thumbImage.Dispose();
            largeImage.Dispose();
            userCroppedImage.Dispose();
            img.Dispose();

            ImagePath.DeleteFile(imageTempFilePath);

            _repoPropertyImages.Insert(propertyId, imageId);
        }


        public void DeleteImage(long propertyId, Guid imageId, HttpContext ctx)
        {
            var imageFileThumb = ctx.Server.MapPath(SiteInfo.PropertyImageFolderPath()) + "\\" + propertyId +
                                   "\\thumb\\t_" + imageId + ".jpg";
            var imageFileLarge = ctx.Server.MapPath(SiteInfo.PropertyImageFolderPath()) + "\\" + propertyId +
                                   "\\large\\" + imageId + ".jpg";

            ImagePath.DeleteFile(imageFileThumb);
            ImagePath.DeleteFile(imageFileLarge);

            _repoPropertyImages.Delete(propertyId, imageId);

        }


    }
}


 