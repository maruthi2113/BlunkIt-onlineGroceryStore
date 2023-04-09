using BlinkIt.Models;
using BlinkIt.Models.ViewModels.category;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlinkIt.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _repo;
        private readonly IPhotoService _photo;

        public CategoryController(ICategoryRepo repo, IPhotoService photo)
        {
            _repo = repo;
            _photo = photo;
        }
        public async Task<IActionResult> Index()
        {
            var results = await _repo.GetAll();
           
            return View(results);
        }

        public async Task<IActionResult> ByName(string s)
        {
            ViewData["byname"] = s;
            return View(await _repo.GetByName(s));
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
                return NotFound();
            return View(result);
        }

        public IActionResult Create()
        {
            var result = new CategoryViewModel
            {

            };
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _photo.AddPhotoAsync(model.Image);
                var category = new Category {
                    Name = model.Name,
                    ImageUrl = result.Url.ToString(),
                };
                _repo.Add(category);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var model = new CategoryEditModel
            {
                Id = id,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = await _repo.GetByIdNoTracking(id);
           
            if (model.Image!= null)
            {
                await _photo.DeletePhotoAsync(category.ImageUrl);
                var inserting = await _photo.AddPhotoAsync(model.Image);
                var imgurl = inserting.Url.ToString();
                var insertcatgory = new Category
                {
                    Id = id,
                    Name = model.Name,
                    ImageUrl = imgurl
                };
                _repo.Update(insertcatgory);
                return RedirectToAction("Index");
            }
            if (category != null)
            {
                category.Id = id;
                category.Name = model.Name;
                category.ImageUrl = category.ImageUrl;
                _repo.Update(category);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repo.GetById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteF(int id,Category model)
        {
            var category= await _repo.GetByIdNoTracking(id);  
           
                await _photo.DeletePhotoAsync(category.ImageUrl);
                _repo.Delete(model);
                return RedirectToAction("Index");
            
            
        }
    }
}
