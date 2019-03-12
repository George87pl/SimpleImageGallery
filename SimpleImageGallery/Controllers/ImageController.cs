using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using SimpleImageGallery.Models;

namespace SimpleImageGallery.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImage _image;
        private IHostingEnvironment _env;

        public ImageController(IImage image, IHostingEnvironment env)
        {
            _image = image;
            _env = env;
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadImageModel uploadImageModel)
        {
            if (ModelState.IsValid)
            {
                var webRoot = _env.WebRootPath;
                var filePath = Path.Combine(webRoot.ToString() + "\\images\\" + uploadImageModel.ImageUpload.FileName);

                if (uploadImageModel.ImageUpload.FileName.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadImageModel.ImageUpload.CopyToAsync(stream);
                    }
                }

                var image = new GalleryImage
                {
                    Title = uploadImageModel.Title,
                    Tags = uploadImageModel.Tags.Split(",").Select(tag => new ImageTag
                    {
                        Description = tag
                    }).ToList(),
                    Created = DateTime.Today,
                    Url = "/images/" + uploadImageModel.ImageUpload.FileName
                };

                _image.AddImage(image);

                return RedirectToAction("Index", "Gallery");
            }

            return View(uploadImageModel);
        }
    }
}