using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.Areas.Admin.Models
{
    public class PropertyAlbumViewModel
    {
        public long PropertyId { get; set; }

        public List<PropertyImageViewModel> AlbumImages { get; set; }
    }
    public class PropertyImageViewModel
    {
        public long PropertyId { get; set; }

        public Guid ImageId { get; set; }

        public bool Visible { get; set; }

        public bool IsDefault { get; set; }
    }
}