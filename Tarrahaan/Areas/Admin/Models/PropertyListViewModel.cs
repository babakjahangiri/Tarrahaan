using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Tarrahaan.Areas.Admin.Models
{
    public class PropertyListPageViewModel
    {
        public List<SelectListItem> ActionList { get; set; }
        public byte SelectedActionId { get; set; }
        public IEnumerable<PropertyListItem> PropertyItem { get; set; }
}

    public class PropertyListItem
    {
        public long Id { get; set; }
        public string ItemCode { get; set; }
        public string ActionName { get; set; }
        public string Title { get; set; }
        public Guid ImageId { get; set; }
        public DateTime PostedDate { get; set; }
    }
}