using BlinkIt.Data;
using BlinkIt.Models;
using BlinkIt.Models.ViewModels.subcategory;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlinkIt.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepo _repo;
        private readonly IPhotoService _photo;
        private readonly ICategoryRepo _repocat;
        public SubCategoryController(ISubCategoryRepo repo,IPhotoService photo,ICategoryRepo cat)
        {
            _repocat = cat;
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
            var results=await _repo.GetByName(s);
            return View(results);
        }
        public async Task<IActionResult> Create()
        {
            var subc = new SubCategorycreateViewModel
            {

            };
            var categories = await _repo.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
           return View(subc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategorycreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _repo.GetCategories();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                return View(model);
            }
            var photoresult = await _photo.AddPhotoAsync(model.Image);

            var subcategory = new SubCategory
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Category = await _repocat.GetById(model.Id),
                ImageUrl = photoresult.Url.ToString()
            };

            _repo.Add(subcategory);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int id)
        {
            var result= await _repo.GetById(id);
            var model = new SubCategoryEditViewModel
            {
                Id = result.Id,
                Name=result.Name,
                Description=result.Description,
                ImageUrl=result.ImageUrl,
                CategoryId=result.CategoryId,
                Category=result.Category,
            };
            var categories = await _repo.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,SubCategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _repo.GetCategories();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                return View(model);
            }
            var subcategory=await _repo.GetByIdNoTracking(id);
            var Imgurl=subcategory.ImageUrl;
            if(model.Image!=null)
            {
                await _photo.DeletePhotoAsync(subcategory.ImageUrl);
               var res= await _photo.AddPhotoAsync(model.Image);
                Imgurl=res.Url.ToString();    
            }
            var catId = model.CategoryId;
            
            if(model.CategoryId!=null&&model.Category!=null)
            {
              
                catId = model.CategoryId;
            }
            var up = new SubCategory
            {
                Id = id,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = Imgurl,
                CategoryId = catId

            };
            _repo.Update(up);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _repo.GetById(id);
            return View(result);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
                return NotFound();
            return View(result);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletef(int id)
        {
            var result = await _repo.GetById(id);
            if(result!=null)
            {
                await _photo.DeletePhotoAsync(result.ImageUrl);
            }
            _repo.Delete(result);
            return RedirectToAction("Index");
            
        }

    }
}
