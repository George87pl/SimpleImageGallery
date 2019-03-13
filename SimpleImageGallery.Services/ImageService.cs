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
            //return GetAll().Where(img => img.Id == id).First();           //pobiera wszystkie pasujące, ale dzięki First(); zwracja pierwszy
            return GetAll().FirstOrDefault(img => img.Id == id);
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll()
                .Where(img => img.Tags
                    .Any(t => t.Description == tag));
        }

        public void AddImage(GalleryImage image)
        {
            _dbContext.GalleryImages.Add(image);
            _dbContext.SaveChanges();
        }

        public void EditImage(GalleryImage image)
        {
            _dbContext.GalleryImages.Update(image);
            _dbContext.SaveChanges();
        }

        public void DeleteImage(GalleryImage image)
        {
            _dbContext.GalleryImages.Remove(image);
            _dbContext.SaveChanges();
        }

        public List<ImageTag> ParseTags(string tags)
        {
            return tags.Split(",").Select(tag => new ImageTag
            {
                Description = tag
            }).ToList();
        }

        public IEnumerable<ImageTag> GetTags()
        {
            return _dbContext.ImageTags.Distinct();
        }
    }
}
