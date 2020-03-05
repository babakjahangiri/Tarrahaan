using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyTypeLangMappingRepository:IPropertyTypeLangMappingRepository
    {

        public string GetName(string langId,int typeId)
        {

            var query = "SELECT Name FROM PropertyTypeLangMapping WHERE TypeId=" + typeId + " AND LangId='" + langId + "'";
            var dbcom = new DbCommand(query);

            if (!dbcom.HasRow()) return String.Empty;

            try
            {
                return (string)dbcom.ExecuteReaderSingleResult();
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyTypeLangMappingRepository > GetName:" + ex.Message);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}