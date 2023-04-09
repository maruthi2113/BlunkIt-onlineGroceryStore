using BlinkIt.Data;
using BlinkIt.Helpers;
using BlinkIt.Models;
using BlinkIt.Models.ProductViewModel;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlinkIt.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo _repo;
        private readonly IPhotoService _photo;
        private readonly ICategoryRepo _repocat;
        private readonly ISubCategoryRepo _reposub;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductController(IProductRepo repo,IPhotoService photo,ICategoryRepo cat,ISubCategoryRepo sub,ApplicationDbContext context,IHttpContextAccessor contextAccessor)
        {
            _repo = repo;
            _photo = photo; 
            _repocat = cat;
            _reposub = sub;
            _context = context; 
            _contextAccessor = contextAccessor; 
        }
        public async Task<IActionResult> Index()
        {
            var results=await _repo.GetAll();   
            return View(results);
        }
        public async Task<IActionResult> GetProductByCategory(int id)
        {
            
            var results = await _repo.GetByCategory(id);
            var subcategories= await _reposub.GetByCategory(id);
            var viewmodel = new ProductwithCategory();
            viewmodel.products = results;
            viewmodel.subCategories=subcategories;
            return View(viewmodel);
        }
        public async Task<IActionResult> GetProductBySubCategory(int id)
        {
            var results= await _repo.GetBySubCategory(id);
            return View(results);
        }
        public async Task<IActionResult> ByName(string s)
        {
            ViewData["byname"] = s;
            var results = await _repo.GetByName(s);
            return View(results);
        }
        
        public async Task<IActionResult> MostlyBought()
        {
            return View();
        }

        public async Task<IActionResult> BySeller()
        {
            var currentuserid = _contextAccessor.HttpContext.User.GetUserId();
            var result = await _repo.GetBySeller(currentuserid);
            return View(result);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var result=await _repo.GetById(id);
            return View(result);
        }

        public IActionResult Create()
        {
            var curUserId = _contextAccessor.HttpContext?.User.GetUserId();
            var productmodel = new ProductCreateViewModel { };
            productmodel.AppUserId=curUserId;
            ViewBag.CategoryId = GetCategories();
            ViewBag.SubCategoryId = GetSubCategories();
            return View(productmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = GetCategories();
                ViewBag.SubCategoryId = GetSubCategories();
                return View(model);
            }

            var ImgUrl = await _photo.AddPhotoAsync(model.Image);
            var product = new Product
            {
                AppUserId=model.AppUserId,
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                count = model.count,
                CategoryId=model.CategoryId,
                SubCategoryId = model.SubCategoryId,
                ImageUrl = ImgUrl.Url.ToString()

            };
            _repo.Add(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _repo.GetById(id);
            var productmodel = new ProductEditViewModel
            {
                AppUserId = product.AppUserId,
                Id = id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,    
                SubCategoryId = product.SubCategoryId,
                Price = product.Price,
                count=product.count,
            };
            ViewBag.CategoryId = GetCategories();
            ViewBag.SubCategoryId = GetSubCategories(product.CategoryId);

            return View(productmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
                {
                ViewBag.CategoryId = GetCategories();
                ViewBag.SubCategoryId = GetSubCategories(model.CategoryId);
                return View(model);
                }
            var tempproduct=await _repo.GetByIdNoTracking(id);
            var ImgUrl = tempproduct.ImageUrl;
            var CatId=tempproduct.CategoryId;
            var SubCatId = tempproduct.SubCategoryId;
            if(model.Image!=null)
            {
                await _photo.DeletePhotoAsync(ImgUrl);
                var res = await _photo.AddPhotoAsync(model.Image);
                ImgUrl = res.Url.ToString();
            }
            

            var product = new Product
            {
                AppUserId=model.AppUserId,
                Id=id,
                Name=model.Name,
                Description=model.Description,
                ImageUrl=ImgUrl,
                Price=model.Price,
                count=model.count,
                CategoryId=model.CategoryId,
                SubCategoryId=model.SubCategoryId,

            };
            _repo.Update(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repo.GetById(id);
            return View(product);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletef(int id,Product product)
        {
            var getproduct=await _repo.GetById(id);
            _photo.DeletePhotoAsync(getproduct.ImageUrl);
            _repo.Delete(getproduct);
            return RedirectToAction("Index");
        }

        private  List<SelectListItem> GetCategories()
        {
            var categorieslist = new List<SelectListItem>();
            List<Category> categories = _context.Categories.ToList();

            categorieslist = categories.Select(ct => new SelectListItem()
            {
                Value=ct.Id.ToString(),
                Text=ct.Name,
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "--Select Category--"
            };
            categorieslist.Insert(0,defItem);
            return categorieslist;
        }

        private List<SelectListItem> GetSubCategories(int categoryid=11)
        {

            List<SelectListItem> subcategorieslist = _context.SubCategories.
                Where(c => c.CategoryId == categoryid).
                OrderBy(n => n.Name).Select(n => new SelectListItem {
                    Value =n.Id.ToString(),
                    Text = n.Name,
                })
                .ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "--Select SubCategory--"
            };
            subcategorieslist.Insert(0, defItem);
            return subcategorieslist;
        }

        [HttpGet]
        public JsonResult GetSubCategoriesByCategory (int categoryId){
        List<SelectListItem> subcategories=GetSubCategories(categoryId);
            return Json(subcategories);
        }

      
    }
}
