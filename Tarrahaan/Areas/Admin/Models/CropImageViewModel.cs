using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.Areas.Admin.Models
{
    public class CropImageViewModel
    {
        public long PropertyId { get; set; }
        public Guid ImageId { get; set; } //Image GUID.ToString
        public double CoordX { get; set; } //Coords y
        public double CoordY { get; set; } //Coords x
        public double CoordY2 { get; set; } //Coords y2
        public double CoordX2 { get; set; } //Coords x2
        public double Width { get; set; }
        public double Height { get; set; }

    }
}