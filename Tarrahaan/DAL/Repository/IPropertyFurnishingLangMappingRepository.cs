using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.DAL.Repository
{
    public interface IPropertyFurnishingLangMappingRepository:IDisposable
    {
        string GetName(string langId, byte furnishingId);

    }
}