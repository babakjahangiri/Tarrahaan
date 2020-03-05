namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyLangMapping")]
    public class PropertyLangMapping
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PropertyId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string LangId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Details { get; set; }

        public bool Complete { get; set; }

        public bool Visible { get; set; }
    }
}
