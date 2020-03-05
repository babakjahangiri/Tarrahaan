using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Tarrahaan.Areas.Admin.Models;
using Tarrahaan.DAL.Entities;
using Tarrahaan.DAL.Repository;
using Tarrahaan.Models;
using Tarrahaan.Models.SiteModels;

namespace Tarrahaan.ServiceLayer
{
    public class SitePageService
    {
        private readonly ISitePageRepository _repoSitePage;
        private readonly ISitePageRepositoryLangMapping _repoSitePageContent;

        public SitePageService() 
        {
            _repoSitePage = new SitePageRepository();
            _repoSitePageContent = new SitePageRepositoryLangMapping();
        }

        public IEnumerable<ShowSitePageViewModel> GetSitePageForUsers(int pageId, string langId)
        {
            var dt = _repoSitePage.GetSitePage(pageId, langId);

            var sitepage = (from DataRow dr in dt.Rows

                           select new ShowSitePageViewModel()
                           {
                               PageId = Convert.ToInt32(dr["PageId"]),
                               LangId = Convert.ToString(dr["LangId"]),
                               PageTitle = Convert.ToString(dr["PageTitle"]),
                               PageContent = Convert.ToString(dr["PageContent"])

                           }).AsEnumerable();

            return sitepage;
        }

        public SitePageViewModel GetSitePageForAdmin(int pageId, string langId)
        {
            var dt = _repoSitePage.GetSitePage(pageId, langId);

            var sitepageEntity = (from DataRow dr in dt.Rows

                            select new SitePageViewModel()
                            {
                                PageId = Convert.ToInt32(dr["PageId"]),
                                LangId = Convert.ToString(dr["LangId"]),
                                PageTitle = Convert.ToString(dr["PageTitle"]),
                                PageContent = Convert.ToString(dr["PageContent"])

                            });

            var sitePageViewModel = new SitePageViewModel();

           var sitepageEntityList = sitepageEntity.ToList();
            for (var i = 0; i < sitepageEntityList.Count; i++)
            {
                if (i != 0) continue;
                sitePageViewModel.PageId = sitepageEntityList[0].PageId;
                sitePageViewModel.LangId = sitepageEntityList[0].LangId;
                sitePageViewModel.PageTitle = sitepageEntityList[0].PageTitle;
                sitePageViewModel.PageContent = sitepageEntityList[0].PageContent;
            }
 
             return sitePageViewModel;
        }

        public bool UpdateSitePageForAdmin(SitePageViewModel sitePage)
        {

            var sitePageContentEntity = new SitePageLangMapping{
                LangId = sitePage.LangId,
                 PageId = sitePage.PageId,
                 PageTitle = sitePage.PageTitle,
                 PageContent = sitePage.PageContent
            };

            try
            {
                return _repoSitePageContent.Update(sitePageContentEntity);
            }
            catch (Exception ex)
            {

                throw new Exception("Update Failed  : " + ex.Message);
            }
          

        }

        public IEnumerable<SitePageListViewModel> GetSitePageList()
        {
           var sitePageListEntity =  _repoSitePage.List();


            var sitePageList = (from item in sitePageListEntity
                                select new SitePageListViewModel()
                                {
                                    Id = item.PageId,
                                    Name = item.Name
                                });

            return sitePageList.AsEnumerable();

        }

    }
}