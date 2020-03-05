using System;
using System.Collections.Generic;
using System.Data;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public interface ITypeCategoryRepository:IDisposable
    {
        TypeCategory Load(byte categoryId);

        DataTable ListDropDownCategories(string langId);

        int GetId(string categoryName);

        string GetName(byte categoryId);
    }
}
