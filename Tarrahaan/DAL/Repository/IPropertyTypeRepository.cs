using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public interface IPropertyTypeRepository: IDisposable
    {
        DataTable ListTypes(string langId, byte categoryId, int parId);
        string GetName(int typeId);
        int GetId(string typeName);
    }

  
}
