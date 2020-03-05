using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Tarrahaan.DAL.Entities;
using Itanc.DbManager.DbData;
using Itanc.DbManager.DbSql;
 

namespace Tarrahaan.DAL.Repository
{

    public class PropertyRepository : IPropertyRepository
    {
       
  
        public Property Load(long propertyId)
        {
            var sqlQuery = " SELECT PropertyId, ItemCode, CategoryId, TypeId, ActionId, Bedroom, Bathroom, Parking, " +
                           " Builtup, LandArea, FurnishingId, PriceRental, PriceDeposite, PriceTotal, CurrencyId, " +
                           " PostedDate, UpdateDate, AdminComment, Visible " +
                           " FROM Property WHERE ( PropertyId = " + propertyId + " )";

            var dbCom = new DbCommand {SqlQuery = sqlQuery};

            var propertyEntity = new Property();

            if (!dbCom.HasRow()) return propertyEntity;

            dbCom.FillRow();
            
          //  Convert.ToInt64()
           
            propertyEntity.PropertyId = Convert.ToInt64(dbCom.Row["PropertyId"]);
            propertyEntity.ItemCode = Convert.ToString(dbCom.Row["ItemCode"]);
            propertyEntity.CategoryId = Convert.ToByte(dbCom.Row["CategoryId"]);
            propertyEntity.TypeId = Convert.ToInt32(dbCom.Row["TypeId"]);
            propertyEntity.ActionId = Convert.ToByte(dbCom.Row["ActionId"]);
            propertyEntity.Bedroom = Convert.ToByte(dbCom.Row["Bedroom"]);
            propertyEntity.Bathroom = Convert.ToByte(dbCom.Row["Bathroom"]);
            propertyEntity.Parking = Convert.ToByte(dbCom.Row["Parking"]);
            propertyEntity.Builtup = (float) Convert.ToDouble(dbCom.Row["Builtup"]);
            propertyEntity.LandArea = (float) Convert.ToDouble(dbCom.Row["LandArea"]);
            propertyEntity.FurnishingId = Convert.ToByte(dbCom.Row["FurnishingId"]);
            propertyEntity.PriceRental = Convert.ToInt64(dbCom.Row["PriceRental"]);
            propertyEntity.PriceDeposite = Convert.ToInt64(dbCom.Row["PriceDeposite"]);
            propertyEntity.PriceTotal = Convert.ToInt64(dbCom.Row["PriceTotal"]);
            propertyEntity.CurrencyId = Convert.ToByte(dbCom.Row["CurrencyId"]);
            propertyEntity.PostedDate = Convert.ToDateTime(dbCom.Row["PostedDate"]);
            propertyEntity.UpdateDate = Convert.ToDateTime(dbCom.Row["UpdateDate"]);
            propertyEntity.AdminComment = Convert.ToString(dbCom.Row["AdminComment"]);
            propertyEntity.Visible = Convert.ToBoolean(dbCom.Row["Visible"]);

            return propertyEntity;
        }

        public List<Property> ListPropertiesforAdmin(byte actionId, int pageSize, int pageNumber, out long count)
        {

            // ------------------- Make Queries ----------------------
            /*1*/
            const string selectQuery = "SELECT Property.PropertyId,Property.ItemCode,Property.ActionId,Property.CategoryId,Property.TypeId,Property.CurrencyId,Property.PostedDate,Property.Visible ";

            /*2*/
            var conditionQuery = " FROM Property WHERE ((ActionId = " + actionId + ") OR (" + actionId + "= 0)) ";

            /*3*/
            const string sortingQuery = " ORDER BY PostedDate DESC ";

            /*4*/
            var pagingQuery = " OFFSET (" + pageSize + " * (" + pageNumber + " - 1)) ROWS " +
                             " FETCH NEXT " + pageSize + " ROWS ONLY";
            // ---------------------------------------------------------

            var sqlQuery = selectQuery + conditionQuery + sortingQuery + pagingQuery;

            var sqlQueryCount = "SELECT COUNT(Property.PropertyId) " + conditionQuery;


            var dbCom = new DbCommand { SqlQuery = sqlQueryCount};

            try
            {
                var countResult = dbCom.ExecuteScalar();
                count = Convert.ToInt64(countResult);
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyRepository -> ListPropertiesforAdmin -> Count : " + ex.Message);
            }

            var dt =new DataTable(); 

            var ddt = new DbDataTable("Property")
            { SqlQuery = sqlQuery };

            try
            {
               dt = ddt.GetTable();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyRepository -> ListPropertiesforAdmin : " + ex.Message);
            }


           // return (from item in dt )

            return DbConvert.ToList<Property>(dt);

        }


        DataTable IPropertyRepository.GetLatestProperties(string langId,int count)
        {
            var sqlQuery = "SELECT TOP (" + count +
                           ") Property.PropertyId,Property.ActionId, PropertyLangMapping.Title As Title, PropertyLangMapping.Details As Description ," +
                           " PropertyActionLangMapping.SellerAction As Action" +
                           " FROM Property INNER JOIN " +
                           " PropertyLangMapping ON Property.PropertyId = PropertyLangMapping.PropertyId " +
                           " INNER JOIN " + 
                           " PropertyActionLangMapping ON Property.ActionId = PropertyActionLangMapping.ActionId" +
                           " WHERE(PropertyLangMapping.LangId = '" + langId +
                           "') AND (Property.Visible=1) AND (PropertyActionLangMapping.LangId = '" + langId + "')  AND (PropertyLangMapping.Visible=1) ORDER BY UpdateDate DESC";

            

            var ddt = new DbDataTable("Property")
            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyRepository -> GetLatestProperties : " + ex.Message);
            }
           
        }

        public DataTable ListPropertiesforUsers(string langId, byte actionId, byte categoryId , int typeId, byte furnishingId ,int pageSize,int pageNumber , out long count)
        {
            const string selectQuery = "SELECT Property.PropertyId, Property.Bedroom, Property.Bathroom, Property.Parking, Property.Builtup, PropertyLangMapping.Title As Title, PropertyLangMapping.Details As Description";
            var conditionJoinQuery = " FROM Property INNER JOIN " +
                                 " PropertyLangMapping ON Property.PropertyId = PropertyLangMapping.PropertyId " +
                                 " WHERE(PropertyLangMapping.LangId = '" + langId + "') AND (ActionId=" + actionId + ") AND " +
                                 " ((TypeId=" + typeId + ") or (0 =" + typeId + ")) AND " +
                                 " ((CategoryId=" + categoryId + ") or (0 =" + categoryId + ")) AND ((FurnishingId=" + furnishingId + ") OR (0 = " + furnishingId + "))" +
                                 " AND (Property.Visible=1) AND (PropertyLangMapping.Visible=1) AND (PropertyLangMapping.Complete=1) AND (PropertyLangMapping.Visible=1) ";

            const string sortingQuery = " ORDER BY PostedDate DESC ";
            var pagingQuery = " OFFSET (" + pageSize + " * (" + pageNumber + " - 1)) ROWS " +
                              " FETCH NEXT " + pageSize  + " ROWS ONLY";

            var sqlQuery = selectQuery + conditionJoinQuery + sortingQuery + pagingQuery;

            var sqlQueryCount = "SELECT COUNT(Property.PropertyId) " + conditionJoinQuery;

             var dbCom = new DbCommand(sqlQueryCount);

            try
            {
                var countResult = dbCom.ExecuteScalar();
                count = Convert.ToInt64(countResult);
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyRepository -> ListPropertiesforUsers -> Count : " + ex.Message);
            }


            var ddt = new DbDataTable("Property")
            { SqlQuery = sqlQuery };

            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyRepository -> ListPropertiesforUsers : " + ex.Message);
            }
           
         }
 

    public DataTable GetProperty(long propertyId, string langId)
     {

            var sqlQuery =
                " SELECT Property.PropertyId, Property.ItemCode,Property.ActionId, Property.Bedroom, Property.Bathroom, Property.Parking, " +
                " Property.Builtup, Property.LandArea, Property.PriceRental,Property.PriceDeposite, Property.PriceTotal, " +
                " Property.PostedDate, Property.UpdateDate , Property.Visible, Property.AdminComment, PropertyActionLangMapping.SellerAction AS Action, PropertyLangMapping.Title, PropertyLangMapping.Details, " +
                " PropertyFurnishingLangMapping.Name AS Furnishing, TypeCategoryLangMapping.Name AS Category, PropertyTypeLangMapping.Name As Type ," +
                " Currency.CurrencySign, Currency.ISOCode As CurrencyIsoCode" + 
                " FROM Property INNER JOIN " +
                " PropertyActionLangMapping ON Property.ActionId = PropertyActionLangMapping.ActionId INNER JOIN " +
                " PropertyLangMapping ON Property.PropertyId = PropertyLangMapping.PropertyId INNER JOIN " +
                " PropertyFurnishingLangMapping ON Property.FurnishingId = PropertyFurnishingLangMapping.FurnishingId INNER JOIN " +
                " TypeCategoryLangMapping ON Property.CategoryId = TypeCategoryLangMapping.CategoryId INNER JOIN " +
                " PropertyTypeLangMapping ON Property.TypeId = PropertyTypeLangMapping.TypeId " +
                " INNER JOIN " +
                " Currency ON Property.CurrencyId = Currency.CurrencyId" + 
                " WHERE (Property.PropertyId = " + propertyId + ") AND " +
                " (PropertyLangMapping.LangId = '" + langId + "') AND " +
                " (PropertyActionLangMapping.LangId = '" + langId + "') AND " +
                " (PropertyFurnishingLangMapping.LangId = '" + langId + "') AND " +
                " (TypeCategoryLangMapping.LangId = '" + langId + "') AND " +
                " (PropertyTypeLangMapping.LangId = '" + langId + "')";

            var dbdt = new DbDataTable(sqlQuery);
         try
            {
                return dbdt.GetTable(0);
            }
        catch (Exception ex)
        {
            throw new Exception("PropertyRepository -> GetProperty(propertyId,langId) : " + ex.Message);
        }
          

        }

        public long GetCount(byte actionId)
        {
            var sqlQuery = "SELECT COUNT(PropertyId) AS Count FROM Property WHERE ((ActionId = " + actionId  + ") OR (" + actionId + " = 0 ))";
            var dbCom = new DbCommand(sqlQuery);

            try
            {
                return Convert.ToInt64(dbCom.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyRepository -> GetCount  : " + ex.Message);
            }

        }

        public long Insert(Property property)
        {

            var queryMaker = new DbSqlQueryMaker { TableName = "Property" };
            queryMaker.ValueParameters.Add(new ColumnItem ("ItemCode", "@ItemCode" ));
            queryMaker.ValueParameters.Add(new ColumnItem("CategoryId", "@CategoryId" ));
            queryMaker.ValueParameters.Add(new ColumnItem("TypeId", "@TypeId" ));
            queryMaker.ValueParameters.Add(new ColumnItem("ActionId", "@ActionId" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Bedroom", "@Bedroom" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Bathroom", "@Bathroom" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Parking", "@Parking" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Builtup", "@Builtup" ));
            queryMaker.ValueParameters.Add(new ColumnItem("LandArea", "@LandArea" ));
            queryMaker.ValueParameters.Add(new ColumnItem("FurnishingId", "@FurnishingId" ));
            queryMaker.ValueParameters.Add(new ColumnItem("PriceRental", "@PriceRental" ));
            queryMaker.ValueParameters.Add(new ColumnItem("PriceDeposite", "@PriceDeposite" ));
            queryMaker.ValueParameters.Add(new ColumnItem("PriceTotal", "@PriceTotal" ));
            queryMaker.ValueParameters.Add(new ColumnItem("CurrencyId", "@CurrencyId" ));
            queryMaker.ValueParameters.Add(new ColumnItem("PostedDate", "@PostedDate" ));
            queryMaker.ValueParameters.Add(new ColumnItem("UpdateDate", "@UpdateDate" ));
            queryMaker.ValueParameters.Add(new ColumnItem("AdminComment", "@AdminComment" ));
            queryMaker.ValueParameters.Add(new ColumnItem("Visible", "@Visible" ));

            var sqlQuery = queryMaker.Insert() + " SELECT SCOPE_IDENTITY()";

            var dbCom = new DbCommand {SqlQuery = sqlQuery};

            dbCom.DbSqlParameters.Add(new DbSqlParameter("@ItemCode", property.ItemCode, SqlDbType.NVarChar));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@CategoryId", property.CategoryId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@TypeId", property.TypeId, SqlDbType.Int));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@ActionId", property.ActionId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Bedroom", property.Bedroom, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Bathroom", property.Bathroom, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Parking", property.Parking, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Builtup", property.Builtup, SqlDbType.Real));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@LandArea", property.LandArea, SqlDbType.Real));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@FurnishingId", property.FurnishingId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PriceRental", property.PriceRental, SqlDbType.BigInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PriceDeposite", property.PriceDeposite, SqlDbType.BigInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PriceTotal", property.PriceTotal, SqlDbType.BigInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@CurrencyId", property.CurrencyId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PostedDate", property.PostedDate, SqlDbType.DateTime));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@UpdateDate", property.UpdateDate, SqlDbType.DateTime));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@AdminComment", property.AdminComment, SqlDbType.NVarChar));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Visible", property.Visible, SqlDbType.Bit));


            try
            {
                 return Convert.ToInt64(dbCom.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyRepository -> Insert  : " + ex.Message);
            }

                
        }

        public bool Update(Property property)
        {

            var queryMaker = new DbSqlQueryMaker { TableName = "Property" };
            queryMaker.ValueParameters.Add(new ColumnItem("ItemCode", "@ItemCode"));
            queryMaker.ValueParameters.Add(new ColumnItem("CategoryId", "@CategoryId"));
            queryMaker.ValueParameters.Add(new ColumnItem("TypeId", "@TypeId"));
            queryMaker.ValueParameters.Add(new ColumnItem("ActionId", "@ActionId"));
            queryMaker.ValueParameters.Add(new ColumnItem("Bedroom", "@Bedroom"));
            queryMaker.ValueParameters.Add(new ColumnItem("Bathroom", "@Bathroom"));
            queryMaker.ValueParameters.Add(new ColumnItem("Parking", "@Parking"));
            queryMaker.ValueParameters.Add(new ColumnItem("Builtup", "@Builtup"));
            queryMaker.ValueParameters.Add(new ColumnItem("LandArea", "@LandArea"));
            queryMaker.ValueParameters.Add(new ColumnItem("FurnishingId", "@FurnishingId"));
            queryMaker.ValueParameters.Add(new ColumnItem("PriceRental", "@PriceRental"));
            queryMaker.ValueParameters.Add(new ColumnItem("PriceDeposite", "@PriceDeposite"));
            queryMaker.ValueParameters.Add(new ColumnItem("PriceTotal", "@PriceTotal"));
            queryMaker.ValueParameters.Add(new ColumnItem("CurrencyId", "@CurrencyId"));
            queryMaker.ValueParameters.Add(new ColumnItem("PostedDate", "@PostedDate"));
            queryMaker.ValueParameters.Add(new ColumnItem("UpdateDate", "@UpdateDate"));
            queryMaker.ValueParameters.Add(new ColumnItem("AdminComment", "@AdminComment"));
            queryMaker.ValueParameters.Add(new ColumnItem("Visible", "@Visible"));
            queryMaker.WhereParameters.Add(new ColumnItem("PropertyId", "@PropertyId"));

            var sqlQuery = queryMaker.Update();

            var dbCom = new DbCommand { SqlQuery = sqlQuery };

            dbCom.DbSqlParameters.Add(new DbSqlParameter("@ItemCode", property.ItemCode, SqlDbType.NVarChar));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@CategoryId", property.CategoryId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@TypeId", property.TypeId, SqlDbType.Int));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@ActionId", property.ActionId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Bedroom", property.Bedroom, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Bathroom", property.Bathroom, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Parking", property.Parking, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Builtup", property.Builtup, SqlDbType.Real));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@LandArea", property.LandArea, SqlDbType.Real));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@FurnishingId", property.FurnishingId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PriceRental", property.PriceRental, SqlDbType.BigInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PriceDeposite", property.PriceDeposite, SqlDbType.BigInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PriceTotal", property.PriceTotal, SqlDbType.BigInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@CurrencyId", property.CurrencyId, SqlDbType.TinyInt));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PostedDate", property.PostedDate, SqlDbType.DateTime));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@UpdateDate", property.UpdateDate, SqlDbType.DateTime));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@AdminComment", property.AdminComment, SqlDbType.NVarChar));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@Visible", property.Visible, SqlDbType.Bit));
            dbCom.DbSqlParameters.Add(new DbSqlParameter("@PropertyId", property.PropertyId, SqlDbType.BigInt));

            var flag = false;

            try
            {
                if (Convert.ToInt32(dbCom.ExecuteNonQuery()) == 1)
                    flag = true;
            }

            catch (Exception ex)
            {
                throw new Exception("PropertyRepository -> Update  : " + ex.Message);
            }

            return flag;
        }

        public bool Delete(long propertyId)
        {
            var queryMaker = new DbSqlQueryMaker { TableName = "Property" };
            queryMaker.WhereParameters.Add(new ColumnItem("PropertyId", propertyId));
            var sqlQuery = queryMaker.Delete();

            var dbCom = new DbCommand(sqlQuery);

            try
            {
                dbCom.ExecuteNonQuery();
             }
            catch (DbManagerException ex)
            {
                throw new Exception("PropertyRepository -> Delete  : " + ex.Message);
            }

            return true;
        }


        #region IDisposable Support


        private bool _disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose manage resources  .Net classes
                }

                // dispose unmanage resources
                
              
                _disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PropertyRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}