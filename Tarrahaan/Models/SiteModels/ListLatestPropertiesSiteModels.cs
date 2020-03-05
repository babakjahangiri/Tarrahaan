using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.Models.SiteModels
{
    public class ListLatestPropertiesSiteModels
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        public byte ActionId { get; set;}

        public Guid ImageId { get; set; }

    }
}