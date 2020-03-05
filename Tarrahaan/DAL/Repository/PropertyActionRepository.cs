using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyActionRepository : IPropertyActionRepository
    {
        
        public DataTable ListActions(string langId)
        {
            var sqlQuery = "SELECT PropertyAction.ActionId AS ActionId, PropertyActionLangMapping.SellerAction As ActionName " +
                          " FROM PropertyAction INNER JOIN " +
                          " PropertyActionLangMapping ON PropertyAction.ActionId = PropertyActionLangMapping.ActionId " +
                          " WHERE (PropertyActionLangMapping.LangId = '" + langId + "')";

            var ddt = new DbDataTable("PropertyAction")
            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyActionRepository -> ListActions : " + ex.Message);
            }
        }

        public byte GetId(string actionName)
        {
            var query = "SELECT ActionId FROM PropertyAction WHERE GeneralAction='" + actionName + "'";
            var dbcom = new DbCommand(query);

            if (dbcom.HasRow())
                return (byte) dbcom.ExecuteReaderSingleResult();
            else
                return 0; // there is no record found
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}