using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyActionLangMappingRepository:IPropertyActionLangMappingRepository
    {
        public string GetSellerActionName(string langId, byte actionId)
        {
            var query = "SELECT SellerAction FROM PropertyActionLangMapping WHERE ActionId=" + actionId + " AND LangId='" + langId + "'";
            var dbcom = new DbCommand(query);

            if (!dbcom.HasRow()) return String.Empty;

            try
            {
                return (string)dbcom.ExecuteReaderSingleResult();
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyActionLangMappingRepository > GetSellerActionName:" + ex.Message);
            }

        }
            

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}