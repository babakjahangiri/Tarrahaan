using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tarrahaan.DAL.Repository;

namespace Tarrahaan.ServiceLayer
{
    public class LocalizationService
    {
        private readonly ITypeCategoryRepository _repoTypeCategory;

        public LocalizationService()
        {
            _repoTypeCategory = new TypeCategoryRepository();
        }



    }
}