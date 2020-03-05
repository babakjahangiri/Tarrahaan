using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.Areas.Admin.Models
{
    public class PropertyViewModel
    {

        public long PropertyId { get; set; }

        [StringLength(10)]
        [Display(Name = @"Item Code : ")]
        public string ItemCode { get; set; }

        [Display(Name = @"Category :")]
        public List<SelectListItem> CategoryList { get; set; }
        public byte SelectedCategoryId { get; set; }

        [Display(Name = @"Category :")]
        public string CategoryName { get; set;}

        [Display(Name = @"Type :")]
        public List<SelectListItem> TypeList { get; set; }
        public int SelectedTypeId { get; set;}

        [Display(Name = @"Property Type :")]
        public string TypeName { get; set; }

        [Display(Name = @"For :")]
        public List<SelectListItem> ActionList { get; set; }
        public byte SelectedActionId { get; set; }

        [Display(Name = @"Property is :")]
        public string ActionName { get; set; }


        [Display(Name = @"Currency :")]
        public List<SelectListItem> CurrencyList { get; set; }
        public byte SelectedCurrencyId { get; set; }

        [Display(Name = @"Selected Currency :")]
        public string CurrencyName { get; set; }


        public byte BedRoomNo { get; set; }

        public byte BathRoomNo { get; set; }

        public byte ParkingNo { get; set; }

        [Display(Name = @"Builtup :")]
        public float Builtup { get; set; }

        [Display(Name = @"Land Area :")]
        public float LandArea { get; set; }

        [Display(Name = @"Furnishing :")]
        public List<SelectListItem> FurnishingList { get; set; }
        public byte SelectedFurnishingId { get; set; }

        [Display(Name = @"Furnishing Status :")]
        public string FurnishingName { get; set; }

        [Display(Name = @"Asking Price :")]
        public string AskingPrice {get; set;}

        [Display(Name = @"Rental Fee(monthly) :")]
        public string RentMonthlyPrice { get; set; }

        [Display(Name = @"Deposit Fee :")]
        public string RentDepositPrice { get; set; }

        [Display(Name = @"Posted Date :")]
        public DateTime PostedDate { get; set; }

        [Display(Name = @"Last Update :")]
        public DateTime LastUpdate { get; set; }

        [Display(Name = @"Admin Comment : ")]
        public string AdminComment { get; set; }

        [Display(Name = @"Visible (check) : ")]
        public bool Visible { get; set; }

        public List<PropertyLangMappingViewModel> PropertyLangMapping { get; set; }

    }


    public class PropertyLangMappingViewModel
    {
        public long PropertyId { get; set; }

        public string LangId { get; set; }

        [Display(Name = @"Title :")]
        public string Title { get; set; }

        [Display(Name = @"Details :")]
        public string Details { get; set; }

        public bool Complete { get; set; }

        public bool Visible { get; set; }

    }
}