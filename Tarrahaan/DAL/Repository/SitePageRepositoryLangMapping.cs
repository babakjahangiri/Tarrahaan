using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;
using Itanc.DbManager.DbSql;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public class SitePageRepositoryLangMapping: ISitePageRepositoryLangMapping
    {
        public bool Update(SitePageLangMapping sitePageLangMapping)
        {
            var queryMaker = new DbSqlQueryMaker { TableName = "SitePageLangMapping" };

            queryMaker.WhereParameters.Add(new ColumnItem("PageId", "@PageId"));
            queryMaker.WhereParameters.Add(new ColumnItem("LangId", "@LangId"));
            queryMaker.ValueParameters.Add(new ColumnItem("PageTitle", "@PageTitle"));
            queryMaker.ValueParameters.Add(new ColumnItem("PageContent", "@PageContent"));
           
            var sqlQuery = queryMaker.Update();

            var dbcom = new DbCommand(sqlQuery);

            dbcom.DbSqlParameters.Add(new DbSqlParameter("@PageId", sitePageLangMapping.PageId, SqlDbType.Int));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@LangId", sitePageLangMapping.LangId, SqlDbType.VarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@PageTitle", sitePageLangMapping.PageTitle, SqlDbType.NVarChar));
            dbcom.DbSqlParameters.Add(new DbSqlParameter("@PageContent", sitePageLangMapping.PageContent, SqlDbType.NVarChar));
 

            var flag = false;
            try
            {
                if (Convert.ToInt32(dbcom.ExecuteNonQuery()) == 1)
                    flag = true;
            }
            catch (Exception ex)
            {

                throw new Exception("SitePageRepositoryLangMapping -> Update  : " + ex.Message);
            }

            return flag;
        }
    }
}