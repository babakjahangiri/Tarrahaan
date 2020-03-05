using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
     public interface IPropertyLangMappingRepository:IDisposable
     {
         PropertyLangMapping Load(long propertyId);

         string GetTitle(long propertyId, string langId);

         bool Insert(PropertyLangMapping propertyContent);

         bool Update(PropertyLangMapping propertyContent);

         bool Delete(long propertyId);
        
         List<PropertyLangMapping> List(long propertyId);

    }
}
