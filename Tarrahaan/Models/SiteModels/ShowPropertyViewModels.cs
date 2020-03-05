using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tarrahaan.Models.SiteModels;

namespace Tarrahaan.Models
{
    public class ShowPropertySiteModel
    {
        public long PropertyId { get; set; }

        public string ItemCode { get; set; }

        public DateTime LastUpdate { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public IList<ShowPropertyImagesViewModel> Images { get; set; } 

        public byte BedRoomNo { get; set; }

        public byte BathRoomNo { get; set; }

        public byte ParkingNo { get; set; }

        public float Builtup { get; set; }

        public float LandArea { get; set; }

        public byte ActionId { get; set; } //Its static set the price according this

        //public byte CategoryId { get; set; }

        //public int TypeId { get; set;}

        public string Action { get; set; } //Its static set the price according this

        public string Category { get; set; }

        public string Type { get; set; }
        
        public string Furnishing { get; set; } //get by join show the status of furnishing

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N0}")]
        public long AskingPrice { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N0}")]
        public long RentMonthlyPrice { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N0}")]
        public long RentDepositPrice { get; set; }

        public string CurrencySign { get; set; }

        public string CurrencyIsoCode { get; set; }

        public bool Visible { get; set; }

    }

    public class ShowPropertyImagesViewModel
    {
        public long PropertyId { get; set; }

        public Guid ImageId { get; set; }
    }

}