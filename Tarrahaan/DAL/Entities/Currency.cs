namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Currency")]
    public class Currency
    {
        public byte CurrencyId { get; set; }

        [Required]
        [StringLength(5)]
        public string CurrencySign { get; set; }

        [Required]
        [StringLength(10)]
        public string IsoCode { get; set; }

        public bool Visible { get; set; }


    }
}
