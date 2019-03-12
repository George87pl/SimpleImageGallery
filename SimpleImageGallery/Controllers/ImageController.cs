using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using SimpleImageGallery.Models;

namespace SimpleImageGallery.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImage _image;

        public ImageController(IImage image)
        {
            _image = image;
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(UploadImageModel uploadImageModel)
        {
            if (ModelState.IsValid)
            {
                var image = new GalleryImage
                {
                    Title = uploadImageModel.Title,
                    Tags = uploadImageModel.Tags.Split(",").Select(tag => new ImageTag
                    {
                        Description = tag
                    }).ToList()
                };

                _image.AddImage(image);
                return RedirectToAction("Upload");
            }

            return View(uploadImageModel);
        }
    }
}