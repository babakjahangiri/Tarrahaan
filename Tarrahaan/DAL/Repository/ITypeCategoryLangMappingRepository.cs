using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarrahaan.DAL.Repository
{
    public interface ITypeCategoryLangMappingRepository:IDisposable
    {
        string GetName(string langId, byte categoryId);
    }
}