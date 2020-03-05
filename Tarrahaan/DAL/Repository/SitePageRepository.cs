using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Itanc.DbManager.DbData;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public class SitePageRepository: ISitePageRepository
    {

      //public SitePage Load(int pageId, string langId)
      //{
            //We Need Domain layer To Pass Join Item To Domain Layer rather Than Entity layer 
            //var sqlQuery =
            //   "SELECT SitePageLangMapping.PageId, SitePageLangMapping.LangId, SitePageLangMapping.PageTitle, SitePageLangMapping.PageContent " +
            //   " FROM SitePageLangMapping INNER JOIN " +
            //   " SitePage ON SitePageLangMapping.PageId = SitePage.PageId " +
            //   "WHERE(SitePage.PageId =" + pageId + " ) AND (SitePageLangMapping.LangId = '" + langId + "')";


            //var dbCom = new DbCommand { SqlQuery = sqlQuery };

            //var sitePageEntity = new SitePage();

            //if (!dbCom.HasRow()) return sitePageEntity;

            //dbCom.FillRow();

            ////  Convert.ToInt64()

            //sitePageEntity.PageId = Convert.ToInt32(dbCom.Row["PageId"]);
            //sitePageEntity.LangId = Convert.ToString(dbCom.Row["LangId"]);
            //sitePageEntity.PageTitle = Convert.ToString(dbCom.Row["PageTitle"]);
            //sitePageEntity.PageContent = Convert.ToString(dbCom.Row["PageContent"]);

            //return sitePageEntity;

     // }


        public DataTable GetSitePage(int pageId, string langId)
        {
            var sqlQuery =
                "SELECT SitePageLangMapping.PageId, SitePageLangMapping.LangId, SitePageLangMapping.PageTitle, SitePageLangMapping.PageContent " +
                " FROM SitePageLangMapping INNER JOIN " +
                " SitePage ON SitePageLangMapping.PageId = SitePage.PageId " +
                "WHERE(SitePage.PageId =" + pageId + " ) AND (SitePageLangMapping.LangId = '" + langId + "')";
                          
            var ddt = new DbDataTable("SitePage")
            { SqlQuery = sqlQuery };
            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {
                throw new Exception("SitePageRepository -> GetSitePage : " + ex.Message);
            }
        }

        public List<SitePage> List()
        {
            const string sqlQuery = "SELECT PageId, Name FROM SitePage ORDER BY Name";

            var ddt = new DbDataTable("SitePage")
            { SqlQuery = sqlQuery };

            DataTable dt;

            try
            {
               dt= ddt.GetTable();
            }
            catch (Exception ex)
            {
                throw new Exception("SitePageRepository -> GetSitePage : " + ex.Message);
            }

            return DbConvert.ToList<SitePage>(dt);

        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}