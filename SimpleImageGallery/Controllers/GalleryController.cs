using Microsoft.AspNetCore.Mvc;
using SimpleImageGallery.Data;
using SimpleImageGallery.Models;
using System.Linq;

namespace SimpleImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImage _imageService;

        public GalleryController(IImage imageService)
        {
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var imageList = _imageService.GetAll();
            var tags = _imageService.GetTags();

            var model = new GalleryIndexModel()
            {
                Images = imageList,
                Tags = tags,                
            };

            return View(model);
        }

        [HttpGet("{id}")]
        public IActionResult Index(string id)
        {
            var imageList = _imageService.GetWithTag(id);
            var tags = _imageService.GetTags();

            var model = new GalleryIndexModel()
            {
                Images = imageList,
                Tags = tags,
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var image = _imageService.GetById(id);

            var model = new GalleryDetailModel()
            {
                Id = image.Id,
                Title = image.Title,
                CreatedOn = image.Created,
                Url = image.Url,
                Tags = image.Tags.Select(t => t.Description).ToList()
            };

            return View(model);
        }
    }
}