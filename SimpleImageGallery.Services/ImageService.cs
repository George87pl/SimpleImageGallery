using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;

namespace SimpleImageGallery.Services
{
    public class ImageService : IImage
    {
        private SimpleImageGalleryDbContext _dbContext;

        public ImageService(SimpleImageGalleryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<GalleryImage> GetAll()
        {
            return _dbContext.GalleryImages
                .Include(img => img.Tags);
        }

        public GalleryImage GetById(int id)
        {
            //return GetAll().Where(img => img.Id == id).First();
            return GetAll().FirstOrDefault(img => img.Id == id);
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll()
                .Where(img => img.Tags
                    .Any(t => t.Description == tag));
        }
    }
}
