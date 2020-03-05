using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
     
        public DataTable ListTypes(string langId, byte categoryId, int parId)
        {
            var sqlQuery =
                "SELECT PropertyType.TypeId, PropertyType.CategoryId, PropertyType.Name, PropertyType.ParId, PropertyType.Visible, PropertyTypeLangMapping.Name AS TypeName " +
                " FROM PropertyType INNER JOIN " +
                " PropertyTypeLangMapping ON PropertyType.TypeId = PropertyTypeLangMapping.TypeId " +
                " WHERE (Visible=1) AND (PropertyTypeLangMapping.LangId = '" + langId + "') AND (PropertyType.ParId = " + parId +
                ") AND (PropertyType.CategoryId = " + categoryId + ")";

            var ddt = new DbDataTable("PropertyType")
            { SqlQuery = sqlQuery };

           return ddt.GetTable();
          
        }

        public string GetName(int typeId)
        {
            var query = "SELECT Name FROM PropertyType WHERE TypeId=" + typeId;
            var dbcom = new DbCommand(query);

            try
            {
                if (dbcom.HasRow())
                {
                    return (string)dbcom.ExecuteReaderSingleResult();
                }
                else { return null; }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetId(string typeName)
        {
            var query = "SELECT TypeId FROM PropertyType WHERE Name='" + typeName + "'";
            var dbcom = new DbCommand(query);

            try
            {
                if (dbcom.HasRow())
                    return (int)dbcom.ExecuteReaderSingleResult();
                else
                {
                    return 0;
                }

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}