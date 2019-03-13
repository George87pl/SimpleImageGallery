using System.Collections.Generic;
using SimpleImageGallery.Data.Models;

namespace SimpleImageGallery.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        GalleryImage GetById(int id);

        void AddImage(GalleryImage image);
        void EditImage(GalleryImage image);
        void DeleteImage(GalleryImage image);

        List<ImageTag> ParseTags(string tags);
        IEnumerable<ImageTag> GetTags();
    }
}
