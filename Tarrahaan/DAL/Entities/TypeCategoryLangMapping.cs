namespace Tarrahaan.DAL.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeCategoryLangMapping")]
    public class TypeCategoryLangMapping
    {

        [Key]
        [Column(Order = 0)]
        public byte CategoryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string LangId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

    }
}
