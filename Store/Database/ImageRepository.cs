using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class ImageRepository : CommonRepository<Image>, IImageRepository
    {
        public ImageRepository(StoreContext context) : base(context.Images)
        {
        }
    }
}
