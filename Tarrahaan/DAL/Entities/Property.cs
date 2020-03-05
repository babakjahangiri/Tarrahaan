namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Property")]
    public class Property
    {

        public Property() :base()
        {
            PropertyImages = new List<PropertyImages>();
        }

         public long PropertyId { get; set; }

        [StringLength(10)]
        public string ItemCode { get; set; }

        public int TypeId { get; set; }

        public byte CategoryId { get; set; }

        public byte ActionId { get; set; }

        public byte Bedroom { get; set; }

        public byte Bathroom { get; set; }

        public byte Parking { get; set; }

        public float Builtup { get; set; }

        public float LandArea { get; set; }

        public byte FurnishingId { get; set; }

        public long PriceRental { get; set; }

        public long PriceDeposite { get; set; }

        public long PriceTotal { get; set; }

        public byte CurrencyId { get; set; }

        public DateTime PostedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string AdminComment { get; set; }

        public bool Visible { get; set; }

        public IEnumerable<PropertyLangMapping> PropertyLangMapping { get; set; }

        public IEnumerable<PropertyImages> PropertyImages { get; set;}
    }
}
