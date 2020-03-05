using System;
using System.Collections.Generic;
using System.Data;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public interface IPropertyFurnishingRepository : IDisposable
    {
        DataTable ListFurnishing(string langId);
    }
}