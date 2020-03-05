using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tarrahaan.DAL.Repository;

namespace Tarrahaan.ServiceLayer
{
    public class SeoService
    {
        private readonly ITypeCategoryLangMappingRepository _repoTypeCategoryLangMapping;
        private readonly IPropertyTypeLangMappingRepository _repoPropertyTypeLangMapping;
        private readonly IPropertyActionLangMappingRepository _repoPropertyActionLangMapping;

        public SeoService()
        {
            _repoTypeCategoryLangMapping= new TypeCategoryLangMappingRepository();
            _repoPropertyTypeLangMapping = new PropertyTypeLangMappingRepository();
            _repoPropertyActionLangMapping = new PropertyActionLangMappingRepository();
        }

        public string GetPropertyActionLocalName(string langId, byte proActionId)
        {
            return _repoPropertyActionLangMapping.GetSellerActionName(langId, proActionId);
        }

        public string GetTypeCategoryLocalName(string langId,byte categoryId)
        {
            return _repoTypeCategoryLangMapping.GetName(langId, categoryId);
        }

        public string GetTypeLocalName(string langId, int typeId)
        {
            return _repoPropertyTypeLangMapping.GetName(langId, typeId);
        }



    }
}