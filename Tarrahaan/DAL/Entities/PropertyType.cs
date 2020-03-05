namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyType")]
    public class PropertyType
    {
     
        public PropertyType()
        {
            PropertyTypeLangMapping = new List<PropertyTypeLangMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        public byte CategoryId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int? ParId { get; set; }

        public bool Visible { get; set; }

        public IEnumerable<PropertyTypeLangMapping> PropertyTypeLangMapping { get; set; }
    }
}
