using BlinkIt.Models;
using BlinkIt.Models.ViewModels.homeViewModels;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlinkIt.Controllers
{

    public class HomeController : Controller
    {
     
        private readonly ICategoryRepo _repocategory;
        private readonly IProductRepo _repoproduct;
        public  HomeController(ICategoryRepo repocategory, IProductRepo repoproduct)
        {
            _repocategory = repocategory;
            _repoproduct = repoproduct;
        }
        public async Task<IActionResult> Index()
        {
            var results = await _repocategory.GetAll();
            var presults = await _repoproduct.GetAll();
            ViewBag.CategoriesforProduct = results;
            var indexviewmodel = new IndexViewModel();
            indexviewmodel.Categories = results;
            indexviewmodel.Products = presults;
            return View(indexviewmodel);
        }
    }
}