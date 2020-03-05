using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.Models
{
    //http://codereview.stackexchange.com/questions/91396/is-it-an-anti-pattern-to-let-viewmodel-fill-itself-from-a-domain-object
    //http://programmers.stackexchange.com/questions/216874/mvc-3-tier-where-viewmodels-come-into-play
    //http://forums.asp.net/t/2055737.aspx?In+MVC+can+ViewModels+access+service+layer+What+is+the+best+practice+

    public class ListPropertiesSiteModels
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid ImageId { get; set; }

        public int ImagesCount { get; set; }

        public byte RoomNo { get; set; }

        public byte BathNo { get; set; }

        public byte ParkingNo { get; set; }
        
        public float Builtup { get; set; }

        public string BuiltupUnit { get; set; }


    }
}