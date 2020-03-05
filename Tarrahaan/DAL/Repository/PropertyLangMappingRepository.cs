using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Tarrahaan.DAL.Entities;
using Itanc.DbManager.DbData;
using Itanc.DbManager.DbSql;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyLangMappingRepository:IPropertyLangMappingRepository
    {
        public PropertyLangMapping Load(long propertyId)
        {
            throw new NotImplementedException();
        }

        public string GetTitle(long propertyId, string langId)
        {
            var sqlQuery = "Select Title From PropertyLangMapping Where PropertyId = " + propertyId + " AND LangId='" +
                           langId + "'";

            var dbCom = new DbCommand(sqlQuery);
            try
            {
              return  (string) dbCom.ExecuteReaderSingleResult();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyLangMappingRepository -> GetTitle  : " + ex.Message);
            }
          
        }

        public bool Insert(PropertyLangMapping propertyContent)
        {
            var queryMaker = new DbSqlQueryMaker { TableName = "PropertyLangMapping" };
            queryMaker.ValueParameters.Add(new ColumnItem("PropertyId", "@PropertyId" ));
            queryMaker.ValueParameters.Add(new ColumnItem("LangId", "@LangId" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Title", "@Title" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Details", "@Details" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Complete", "@Complete"));
            queryMaker.ValueParameters.Add(new ColumnItem("Visible", "@Visible"));
           
            var sqlQuery = queryMaker.Insert();

            var dbcom = new DbCommand(sqlQuery);

            dbcom.DbSqlParameters.Add(new DbSqlParameter("@PropertyId", propertyContent.PropertyId, SqlDbType.BigInt));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@LangId", propertyContent.LangId, SqlDbType.VarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Title", propertyContent.Title, SqlDbType.NVarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Details", propertyContent.Details, SqlDbType.NVarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Complete", propertyContent.Complete, SqlDbType.Bit));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Visible", propertyContent.Visible, SqlDbType.Bit));

            int insertResult;

            try
            {
                insertResult = Convert.ToInt32(dbcom.ExecuteNonQuery());
            }

            catch (Exception ex)
            {
                throw new Exception("PropertyLangMappingRepository -> Insert  : " + ex.Message);
            }

            return insertResult == 1;
        }

        public bool Update(PropertyLangMapping propertyContent)
        {

            var queryMaker = new DbSqlQueryMaker { TableName = "PropertyLangMapping" };

            queryMaker.WhereParameters.Add(new ColumnItem("PropertyId", "@PropertyId"));
            queryMaker.WhereParameters.Add(new ColumnItem("LangId", "@LangId"));
            queryMaker.ValueParameters.Add(new ColumnItem("Title", "@Title"));
            queryMaker.ValueParameters.Add(new ColumnItem("Details", "@Details"));
            queryMaker.ValueParameters.Add(new ColumnItem("Complete", "@Complete"));
            queryMaker.ValueParameters.Add(new ColumnItem("Visible", "@Visible"));
           
            var sqlQuery = queryMaker.Update();

            var dbcom = new DbCommand(sqlQuery);

            dbcom.DbSqlParameters.Add(new DbSqlParameter("@PropertyId", propertyContent.PropertyId, SqlDbType.BigInt));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@LangId", propertyContent.LangId, SqlDbType.VarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Title", propertyContent.Title, SqlDbType.NVarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Details", propertyContent.Details, SqlDbType.NVarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Complete", propertyContent.Complete, SqlDbType.Bit));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@Visible", propertyContent.Visible, SqlDbType.Bit));

            var flag = false;

            try
            {
                if (Convert.ToInt32(dbcom.ExecuteNonQuery()) == 1)
                    flag = true;
            }

            catch (Exception ex)
            {
                throw new Exception("PropertyLangMappingRepository -> Update  : " + ex.Message);
            }

            return flag;
        }

        public bool Delete(long propertyId)
        {
            //Delete Two language Record ..
            var queryMaker = new DbSqlQueryMaker { TableName = "PropertyLangMapping" };
            queryMaker.WhereParameters.Add(new ColumnItem("PropertyId", propertyId));
            var sqlQuery = queryMaker.Delete();

            var dbCom = new DbCommand(sqlQuery);

            try
            {
                dbCom.ExecuteNonQuery(); //2 records will be Deleted, lang= fa and lang=en
                 
            }
            catch (DbManagerException ex)
            {
                
                throw new Exception("PropertyLangMappingRepository -> Delete  : " + ex.Message);
            }

            return true;
        }

        public List<PropertyLangMapping> List(long propertyId)
        {
            var sqlQuery = "SELECT PropertyId, LangId, Title, Details, Complete, Visible " +
                           " FROM PropertyLangMapping WHERE(PropertyId = " + propertyId + ")";

            var ddt = new DbDataTable("PropertyLangMapping")
            { SqlQuery = sqlQuery };
            try
            {
                return DbConvert.ToList<PropertyLangMapping>(ddt.GetTable());
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyLangMappingRepository -> List : " + ex.Message);
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}