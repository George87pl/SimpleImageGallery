using SimpleImageGallery.Data.Models;
using System.Collections.Generic;

namespace SimpleImageGallery.Models
{
    public class GalleryIndexModel
    {
        public IEnumerable<GalleryImage> Images { get; set; }
        public IEnumerable<ImageTag> Tags { get; set; }
    };
}
