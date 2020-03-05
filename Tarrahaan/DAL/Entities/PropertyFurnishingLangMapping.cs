namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyFurnishingLangMapping")]
    public class PropertyFurnishingLangMapping
    {
        [Key]
        [Column(Order = 0)]
        public byte FurnishingId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string LangId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

    }
}
