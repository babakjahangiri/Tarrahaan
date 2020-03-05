namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyAction")]
    public class PropertyAction
    {
        public PropertyAction()
        {
            PropertyActionLangMapping = new List<PropertyActionLangMapping>();
        }

        [Key]
        public byte ActionId { get; set; }

        [Required]
        [StringLength(10)]
        public string SellerAction { get; set; }

        [Required]
        [StringLength(10)]
        public string BuyerAction { get; set; }

        [Required]
        [StringLength(10)]
        public string GeneralAction { get; set; }

        public bool? Visible { get; set; }

        public IEnumerable<PropertyActionLangMapping> PropertyActionLangMapping { get; set; }
    }
}
