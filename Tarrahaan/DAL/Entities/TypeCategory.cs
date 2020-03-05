namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("TypeCategory")]
    public class TypeCategory
    {
  
        public TypeCategory() : base()
        {
            TypeCategoryLangMapping = new List<TypeCategoryLangMapping>();
        }

        [Key]
        public byte CategoryId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public bool Visible { get; set; }

        public IEnumerable<TypeCategoryLangMapping> TypeCategoryLangMapping { get; set; }
    }
}
