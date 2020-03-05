namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyFurnishing")]
    public class PropertyFurnishing
    {
      
        public PropertyFurnishing()
        {
            PropertyFurnishingLangMapping = new List<PropertyFurnishingLangMapping>();
        }

        [Key]
        public byte FurnishingId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public bool Visible { get; set; }

        public IEnumerable<PropertyFurnishingLangMapping> PropertyFurnishingLangMapping { get; set; }
    }
}
