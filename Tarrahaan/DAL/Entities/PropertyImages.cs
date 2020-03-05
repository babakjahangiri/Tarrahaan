namespace Tarrahaan.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class PropertyImages
    {

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PropertyId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ImageId { get; set; }

        public bool Visible { get; set; }

        public bool IsDefault { get; set; }

    }
}
