namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SitePage")]
    public class SitePage
    {
  
        public SitePage()
        {
            SitePageLangMapping = new List<SitePageLangMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PageId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public  IEnumerable<SitePageLangMapping> SitePageLangMapping { get; set; }
    }
}
