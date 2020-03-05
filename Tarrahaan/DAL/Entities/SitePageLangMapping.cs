namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SitePageLangMapping")]
    public class SitePageLangMapping
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PageId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string LangId { get; set; }

        [Required]
        [StringLength(50)]
        public string PageTitle { get; set; }

        [Required]
        public string PageContent { get; set; }

    }
}
