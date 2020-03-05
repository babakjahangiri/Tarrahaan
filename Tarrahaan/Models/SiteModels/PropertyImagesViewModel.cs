using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.Models.SiteModels
{
    public class PropertyImagesViewModel
    {
        public long PropertyId { get; set; }

        public Guid ImageId { get; set; }

        public bool IsDefault { get; set; }

        public bool Visible { get; set; }
    }
}