using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarrahaan.DAL.Repository
{
    public interface IPropertyImagesRepository : IDisposable
    {
        DataTable GetImages(long propertyId);

        DataTable GetVisibleImages(long propertyId);

        DataTable GetDefaultImage(long propertyId);

        Guid GetDefaultImageId(long propertyId);

        int GetImagesCount(long propertyId);

        void UnsetAllDefaultImage(long propertyId);

        void SetDefaultImage(long propertyId, Guid imageId);

        void Insert(long propertyId, Guid imageId);

        void Delete(long propertyId, Guid imageId);

        void Delete(long propertyId);



    }
}
