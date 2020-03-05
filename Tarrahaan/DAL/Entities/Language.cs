namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Language")]
    public class Language
    {
        [Key]
        [StringLength(2)]
        public string LangId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public bool Visible { get; set; }

        [Required]
        [StringLength(3)]
        public string Direction { get; set; }
    }
}
