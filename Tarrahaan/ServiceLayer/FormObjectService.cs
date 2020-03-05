using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.DAL.Entities;
using Tarrahaan.DAL.Repository;
using Tarrahaan.Models;
using Tarrahaan.ServiceLayer;

namespace Tarrahaan.ServiceLayer
{
    public class FormObjectService
    {
        private readonly IPropertyTypeRepository _repoType;
        private readonly ITypeCategoryRepository _repoTypeCategory;
        private readonly IPropertyFurnishingRepository _repoFurnishing;
        private readonly IPropertyActionRepository _repoAction;
        private readonly ICurrencyRepository _repoCurrency;

        public FormObjectService()
        {
            _repoType = new PropertyTypeRepository();
            _repoTypeCategory = new TypeCategoryRepository();
            _repoFurnishing = new PropertyFurnishingRepository();
            _repoAction = new PropertyActionRepository();
            _repoCurrency = new CurrencyRepository();

        }

        public List<SelectListItem> DropDwonType(string langId,byte categoryId,int selectedId)
        {
            const int parId = 0;

           var dt = _repoType.ListTypes(langId, categoryId,parId);

           var typelist = (from DataRow dr in dt.Rows
                            select new SelectListItem()
                            {
                                Value = Convert.ToString(dr["TypeId"]),
                                Text = Convert.ToString(dr["TypeName"]),
                                Selected = (Convert.ToString(dr["TypeId"]) == Convert.ToString(selectedId) ? true : false)
                            }).ToList();

            return typelist;
        }

        public List<SelectListItem> DropDwonCategory(string langId,byte selectedId)
        {
            var dt = _repoTypeCategory.ListDropDownCategories(langId);

           //var categorylist = new List<SelectListItem>();

            //var categorylistdefault =  new SelectListItem()
            // {
            //     Value = Convert.ToString(0),
            //     Text = @"----- " + Tarrahaan.Resources.Global.all + @" -----",
            //     Selected = ("" == selected ? true : false)
            // };

            // categorylist. = categorylistdefault;

           var categorylist = (from DataRow dr in dt.Rows
                               select new SelectListItem()
                  {
                         Value = Convert.ToString(dr["CategoryId"]),
                         Text = Convert.ToString(dr["CategoryName"]),
                         Selected = (Convert.ToString(dr["CategoryId"]) == Convert.ToString(selectedId) ? true : false)
                   }).ToList();


         //  return new SelectList(categorylist.Concat(categorylist), "Value", "Text").AsEnumerable();

             return categorylist;
        }

        public List<SelectListItem> DropDwonFurnishing(string langId, int selectedId)
        {
            
            var dt = _repoFurnishing.ListFurnishing(langId);
              
           // var furnishinglist = new List<SelectListItem>();

            var furnishinglist = (from DataRow dr in dt.Rows
                select new SelectListItem()
                {
                    Value = Convert.ToString(dr["FurnishingId"]),
                    Text = Convert.ToString(dr["FurnishingName"]),
                    Selected = (Convert.ToString(dr["FurnishingId"]) == Convert.ToString(selectedId) ? true : false)
                }).ToList();

            return furnishinglist;
        }

        public List<SelectListItem> ListAction(string langId, int selectedId)
        {

            var dt = _repoAction.ListActions(langId);

          //  var actionlist = new List<SelectListItem>();

            var actionlist = (from DataRow dr in dt.Rows
                              select new SelectListItem()
                              {
                                  Value = Convert.ToString(dr["ActionId"]),
                                  Text = Convert.ToString(dr["ActionName"]),
                                  Selected = (Convert.ToString(dr["ActionId"]) == Convert.ToString(selectedId) ? true : false)
                              }).ToList();

            return actionlist;
        }

        public List<SelectListItem> DropDownCurrency(int selectedId)
        {
            var dt = _repoCurrency.ListCurrencies();

            var currencylist = (from DataRow dr in dt.Rows
                select new SelectListItem()
                {
                    Value = Convert.ToString(dr["CurrencyId"]),
                    Text = Convert.ToString(dr["CurrencySign"]),
                    Selected = (Convert.ToString(dr["CurrencyId"]) == Convert.ToString(selectedId) ? true : false)
                }).ToList();

            return currencylist;
        }

    }
}


