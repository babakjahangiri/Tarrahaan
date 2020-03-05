using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public class TypeCategoryRepository:ITypeCategoryRepository
    {
        public TypeCategory Load(byte categoryId)
        {
            var typecategory = new Entities.TypeCategory();

            var sqlQuery = "SELECT CategoryId, Name, Visible FROM TypeCategory WHERE(CategoryId = " + categoryId + ")";
            var dbcmd = new DbCommand(sqlQuery);
            if (dbcmd.HasRow())
            {
                dbcmd.FillRow();
                typecategory.CategoryId = Convert.ToByte(dbcmd.Row["CategoryId"]);
                typecategory.Name = Convert.ToString(dbcmd.Row["Name"]);
                typecategory.Visible = Convert.ToBoolean(dbcmd.Row["Visible"]);
                
                //  typecategory.TypeCategoryLangMapping.Add(new TypeCategoryLangMapping());
                // we Can use this if Create  agument constructor for TypeCategoryLangMapping
                // typecategory.TypeCategoryLangMapping = ;

                //Read From an ICollection :
                // List<TypeCategoryLangMapping> dlist = new List<TypeCategoryLangMapping>(typecategory.TypeCategoryLangMapping);
             
            // Collection<TypeCategoryLangMapping> mycol = new Collection<TypeCategoryLangMapping>();

            }

            return typecategory;
        }

        public DataTable ListDropDownCategories(string langId)
        {
            var sqlQuery = "SELECT TypeCategory.CategoryId AS CategoryId, TypeCategoryLangMapping.Name As CategoryName" +
                           " FROM TypeCategory INNER JOIN " +
                           " TypeCategoryLangMapping ON TypeCategory.CategoryId = TypeCategoryLangMapping.CategoryId " +
                           " WHERE (TypeCategoryLangMapping.LangId = '" + langId + "') AND (TypeCategory.Visible = 1)";

            var ddt = new DbDataTable("TypeCategory")
            { SqlQuery = sqlQuery };

            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {

                throw new Exception("TypeCategoryRepository  -> ListDropDownCategories : " + ex.Message);
            }
           
        }

        public int GetId(string categoryName)
        {
            var query = "SELECT CategoryId FROM TypeCategory WHERE Name='" + categoryName + "'";
            var dbcom = new DbCommand(query);

            try
            {
                if (dbcom.HasRow()== true)
                    return (byte) dbcom.ExecuteReaderSingleResult();
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

        public string GetName(byte categoryId)
        {
            var query = "SELECT Name FROM TypeCategory WHERE CategoryId=" + categoryId ;
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
                throw new Exception("Repository > TypeCategoryRepository (GetName):" + ex.Message);
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}