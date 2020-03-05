using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Mvc.AlternateType;
using Tarrahaan.DAL.Entities;


namespace Tarrahaan.DAL.Repository
{
    public interface IPropertyRepository:IDisposable
    {
        Property Load(long propertyId);

        DataTable GetProperty(long propertyId,string langId);

        DataTable ListPropertiesforUsers(string lang, byte actionId, byte categoryId, int typeId,byte furnishingId, int pageSize, int pageNumber,out long count);

        List<Property> ListPropertiesforAdmin(byte actionId, int pageSize, int pageNumber, out long count);

        DataTable GetLatestProperties(string langId, int count);

        long GetCount(byte actionId);

        long Insert(Property property);

        bool Update(Property property);

        bool Delete(long propertyId);

    }
}
