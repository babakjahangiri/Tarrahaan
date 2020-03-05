using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;

namespace Tarrahaan.DAL.Repository
{
    public class TypeCategoryLangMappingRepository : ITypeCategoryLangMappingRepository

    {
        
        public string GetName(string langId,byte categoryId)
        {

            var query = "SELECT Name FROM TypeCategoryLangMapping WHERE CategoryId=" + categoryId + " AND LangId='" + langId + "'";
            var dbcom = new DbCommand(query);

            if (!dbcom.HasRow()) return String.Empty;

            try
            {
                return (string)dbcom.ExecuteReaderSingleResult();
            }
            catch (Exception ex)
            {
                throw new Exception("TypeCategoryLangMappingRepository > GetName:" + ex.Message);
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        
    }
}