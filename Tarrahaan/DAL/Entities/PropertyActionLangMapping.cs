namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyActionLangMapping")]
    public class PropertyActionLangMapping
    {
        [Key]
        [Column(Order = 0)]
        public byte ActionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string LangId { get; set; }

        [Required]
        [StringLength(15)]
        public string SellerAction { get; set; }

        [Required]
        [StringLength(15)]
        public string BuyerAction { get; set; }

    }
}
