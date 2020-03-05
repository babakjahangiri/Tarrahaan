using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Itanc.DbManager.DbData;
using System.Web;
using Itanc.DbManager.DbSql;

namespace Tarrahaan.DAL.Repository
{
    public class PropertyImagesRepository : IPropertyImagesRepository
    {

        public DataTable GetImages(long propertyId)
        {
            var sqlQuery = " SELECT PropertyId ,ImageId, Visible, IsDefault " +
                           " FROM PropertyImages" +
                           " WHERE (PropertyId = " + propertyId + " ) " +
                           " ORDER BY IsDefault DESC";

            var ddt = new DbDataTable("PropertyImages")
            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> GetImages : " + ex.Message);
            }
        }

        public DataTable GetVisibleImages(long propertyId)
        {
            var sqlQuery = " SELECT PropertyId ,ImageId, Visible, IsDefault " +
                           " FROM PropertyImages" +
                           " WHERE (PropertyId =" + propertyId + ") AND (Visible = 1) " +
                           " ORDER BY IsDefault DESC";

            var ddt = new DbDataTable("PropertyImages")
            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> GetVisibleImages : " + ex.Message);
            }
        }

        public DataTable GetDefaultImage(long propertyId)
        {
            var sqlQuery = " SELECT PropertyId ,ImageId, Visible, IsDefault " +
                           " FROM PropertyImages" +
                           " WHERE (PropertyId =" + propertyId + ") AND (Visible = 1) AND (IsDefault = 1) ";

            var ddt = new DbDataTable("PropertyImages")
            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable(0);
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> GetDefaultImage : " + ex.Message);
            }
        }

        public void UnsetAllDefaultImage(long propertyId)
        {
            var queryMaker = new DbSqlQueryMaker { TableName = "PropertyImages" };
            queryMaker.ValueParameters.Add(new ColumnItem("IsDefault", false));
            queryMaker.WhereParameters.Add(new ColumnItem("propertyId", propertyId));
            var sqlQuery = queryMaker.Update();
            var dbCom = new DbCommand(sqlQuery);
            try
            {
                dbCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> UnsetAllDefaultImage : " + ex.Message);
            }
        }

        public void SetDefaultImage(long propertyId, Guid imageId)
        {
            var queryMaker = new DbSqlQueryMaker {TableName = "PropertyImages"};

            queryMaker.ValueParameters.Add(new ColumnItem("IsDefault", true));
            queryMaker.WhereParameters.Add(new ColumnItem("propertyId", propertyId)); 
            queryMaker.WhereParameters.Add(new ColumnItem("ImageId", imageId));
            var sqlQuery = queryMaker.Update();

            var dbCom = new DbCommand(sqlQuery);
         

            try
            {
                 dbCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> SetDefaultImage : " + ex.Message);
            }

        }


        public Guid GetDefaultImageId(long propertyId)
        {
            var sqlQuery = " SELECT ImageId " +
                           " FROM PropertyImages" +
                           " WHERE (PropertyId =" + propertyId + ") AND (Visible = 1) AND (IsDefault = 1) ";

            var ddt = new DbDataTable("PropertyImages")


            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable(0).Rows.Count > 0 ? Guid.Parse(Convert.ToString(ddt.GetTable(0).Rows[0]["ImageId"])) : Guid.Empty;
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> GetDefaultImageId : " + ex.Message);
            }
        }

        public int GetImagesCount(long propertyId)
        {
            var sqlQueryCount = "SELECT COUNT(PropertyId) FROM PropertyImages WHERE PropertyId =" + propertyId + " AND (Visible = 1) ";

            var dbcom = new DbCommand(sqlQueryCount);

            try
            {
                var countResult = dbcom.ExecuteScalar();
                return Convert.ToInt32(countResult);
            }
            catch (Exception ex)
            {
                throw new Exception("PropertyImagesRepository -> GetImagesCount : " + ex.Message);
            }
        }


        public void Insert(long propertyId, Guid imageId)
        {
            var queryMaker = new DbSqlQueryMaker { TableName = "PropertyImages" };

            queryMaker.ValueParameters.Add(new ColumnItem("propertyId", propertyId));
            queryMaker.ValueParameters.Add(new ColumnItem("ImageId", imageId));
            queryMaker.ValueParameters.Add(new ColumnItem("IsDefault", false));
            queryMaker.ValueParameters.Add(new ColumnItem("Visible", true));

            var sqlQuery = queryMaker.Insert();

            var dbCom = new DbCommand(sqlQuery);

            try
            {
                dbCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> Insert : " + ex.Message);
            }
        }

        public void Delete(long propertyId, Guid imageId)
        {
            var queryMaker = new DbSqlQueryMaker {TableName = "PropertyImages"};

            queryMaker.WhereParameters.Add(new ColumnItem("propertyId", propertyId));
            queryMaker.WhereParameters.Add(new ColumnItem("ImageId", imageId));

            var sqlQuery = queryMaker.Delete();

            var dbCom = new DbCommand(sqlQuery);

            try
            {
                dbCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> Delete : " + ex.Message);
            }
        }

        public void Delete(long propertyId)
        {
            var queryMaker = new DbSqlQueryMaker { TableName = "PropertyImages" };

            queryMaker.WhereParameters.Add(new ColumnItem("propertyId", propertyId));
            var sqlQuery = queryMaker.Delete();

            var dbCom = new DbCommand(sqlQuery);

            try
            {
                dbCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("PropertyImagesRepository -> Delete All Images : " + ex.Message);
            }
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}