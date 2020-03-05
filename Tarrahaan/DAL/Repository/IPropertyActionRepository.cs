using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itanc.DbManager.DbData;
using Tarrahaan.DAL.Entities;
 

namespace Tarrahaan.DAL.Repository
{
    public interface IPropertyActionRepository : IDisposable
    {
        DataTable ListActions(string langId);

        byte GetId(string actionName);

  

    }
}