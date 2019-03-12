using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SimpleImageGallery.Models
{
    public class UploadImageModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Tags { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImageUpload { get; set; }
    }
}
