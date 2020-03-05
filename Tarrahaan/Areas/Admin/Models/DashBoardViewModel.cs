using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.Areas.Admin.Models
{
    public class DashBoardViewModel
    {
        public long TotalCount { get; set; }
        public long SaleCount { get; set; }
        public long RentCount { get; set; }

    }
}