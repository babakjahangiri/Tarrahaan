namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyTypeLangMapping")]
    public class PropertyTypeLangMapping
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string LangId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

    }
}
