using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyFurnishingLangMappingRepository:IPropertyFurnishingLangMappingRepository
    {
        public string GetName(string langId, byte furnishingId)
        {

            var query = "SELECT Name FROM PropertyFurnishingLangMapping WHERE FurnishingId=" + furnishingId + " AND LangId='" + langId + "'";
            var dbcom = new DbCommand(query);

            if (!dbcom.HasRow()) return String.Empty;

            try
            {
                return (string)dbcom.ExecuteReaderSingleResult();
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyFurnishingRepository > GetName:" + ex.Message);
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}