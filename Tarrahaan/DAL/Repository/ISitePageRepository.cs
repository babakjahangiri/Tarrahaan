using System;
using System.Collections.Generic;
using System.Data;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public interface ISitePageRepository : IDisposable
    {
        DataTable GetSitePage(int pageId, string langId);

        List<SitePage> List();

    }
}
