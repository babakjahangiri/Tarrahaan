using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Tarrahaan.DAL.Entities;
using Itanc.DbManager.DbData;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyFurnishingRepository:IPropertyFurnishingRepository
    {
        public DataTable ListFurnishing(string langId)
        {
            var sqlQuery = "SELECT PropertyFurnishing.FurnishingId AS FurnishingId, PropertyFurnishingLangMapping.Name As FurnishingName " +
                           " FROM PropertyFurnishing INNER JOIN " +
                           " PropertyFurnishingLangMapping ON PropertyFurnishing.FurnishingId = PropertyFurnishingLangMapping.FurnishingId " +
                           " WHERE (PropertyFurnishingLangMapping.LangId = '" + langId + "') AND (PropertyFurnishing.Visible = 1) ";

            var ddt = new DbDataTable("PropertyFurnishing")
            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {
                
                throw new Exception("PropertyFurnishingRepository -> ListFurnishing : " + ex.Message);
            }
           
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}