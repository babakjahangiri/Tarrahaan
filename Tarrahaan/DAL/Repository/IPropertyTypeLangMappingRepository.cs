using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarrahaan.DAL.Repository
{
   public interface IPropertyTypeLangMappingRepository:IDisposable
    {
        string GetName(string langId, int typeId);

    }
}
