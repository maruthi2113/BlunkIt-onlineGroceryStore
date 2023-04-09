using BlinkIt.Data;
using BlinkIt.Helpers;
using BlinkIt.Models;
using BlinkIt.Models.ViewModels.Account;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;

namespace BlinkIt.Controllers
{
    public class AccountController : Controller
    {
       
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IAccountRepo _repoAc;
        private readonly IPhotoService _photo;
        private readonly IOrderreceiveditemsRepo _receiveditems;
        private readonly IOrderReceivedRepo _receivedOrder;
        private readonly IOrderPlacedItemsRepo _placedorderitems;
        private readonly IOrderPlacedRepo _Orderplaced;
        private readonly IHttpContextAccessor _contextAccessor;
        public AccountController(UserManager<AppUser> userManager, IOrderPlacedItemsRepo placeditems,IOrderReceivedRepo receivedOrder,IOrderreceiveditemsRepo orderreceiveditems,
            SignInManager<AppUser> signInManager, ApplicationDbContext context,IHttpContextAccessor contextAccessor,IAccountRepo repoac,IPhotoService photo,IOrderPlacedRepo placedRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _repoAc=repoac;
            _contextAccessor = contextAccessor;
            _photo=photo;
            _Orderplaced=placedRepo;
            _receivedOrder = receivedOrder;
            _receiveditems = orderreceiveditems;
            _placedorderitems = placeditems;
            
        }   

        public List<SelectListItem> GetRoles()
        {
            var roleslist = new List<SelectListItem>();
            var def1 = new SelectListItem()
            {
                Value = "User",
                Text = "User"
            };
            var def2 = new SelectListItem()
            {
                Value = "Seller",
                Text = "Seller"
            };
            roleslist.Add(def1);
            roleslist.Add(def2);
            return roleslist;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            /// to check wether email exist or not
            /// below function returns boolean
            var checkuser= await _userManager.FindByEmailAsync(model.Email);
            if (checkuser != null)
            {
                // if user exists the get password
                var getpassword = await _userManager.CheckPasswordAsync(checkuser,model.Password);
                if (getpassword)
                {
                    var checkpassword = await _signInManager.PasswordSignInAsync(checkuser, model.Password, false, false);
                    if (checkpassword.Succeeded)
                        return RedirectToAction("Index", "Product");
                }
                TempData["Error"] = "Incorrect Details";
                return View(model);
            }
            TempData["Error"] = "Incorrect Details";
            return View(model);
        }

        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            ViewBag.RolesId = GetRoles();
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.RolesId = GetRoles();
                return View(model);
            }
            var checkuser = await _userManager.FindByEmailAsync(model.Email);
            if(checkuser!=null)
            {
                TempData["Error"] = "this email address is taken already";
                ViewBag.RolesId = GetRoles();
                return View(model);
            }
            var newuser = new AppUser
            {
                Email = model.Email,
                UserName=model.Name,
            };
            var newresponse=await _userManager.CreateAsync(newuser, model.Password);

            if (newresponse.Succeeded)
            {
                if (model.Role=="Seller")
                    await _userManager.AddToRoleAsync(newuser,UserRoles.Seller);
                    await _userManager.AddToRoleAsync(newuser,UserRoles.User);

            }
            return RedirectToAction("Index","Product");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> ChangePassword()
        {
            var result = new Changepassword { };
                return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(Changepassword model)
        {
            if (!ModelState.IsValid)
                return View();
            var currentuserId =  _contextAccessor.HttpContext.User.GetUserId();
            var currntuser = await _repoAc.GetById(currentuserId);
            //var currntuserpasssword= await _userManager

            var removepassword = await _userManager.RemovePasswordAsync(currntuser);
            if (removepassword.Succeeded)
            {
                var result = await _userManager.AddPasswordAsync(currntuser, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    TempData["Error"] = "cant change password";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            return View(model);
        }
       
        public async Task<IActionResult> Forgotpassword()
        {
            var model = new Forgotmodel { };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(Forgotmodel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var emailreult= await _userManager.FindByEmailAsync(model.Email);
            if (emailreult != null)
            {
                var deletepassword= await _userManager.RemovePasswordAsync(emailreult);
                if(deletepassword.Succeeded)
                {
                    var addpassword = await _userManager.AddPasswordAsync(emailreult,model.Password);
                    if(addpassword.Succeeded)
                    {
                        return RedirectToAction("Login","Account");
                    }
                    else
                    {
                        TempData["Error"] = "failed to reset the password";
                    }
                }
            }
            else
            {
                ViewData["Error"] = "invalid email";
                return View(model);
            }
            return RedirectToAction("Login","Account");
        }

        public async Task<IActionResult> Profile()
        {
            var currentuserId = _contextAccessor.HttpContext.User.GetUserId();
            var currentuser = await _repoAc.GetById(currentuserId);

            return View(currentuser);
        }
        public  async Task<IActionResult> EditProfile()
        {
            var id =_contextAccessor.HttpContext.User.GetUserId().ToString();
            var currentuser = await _repoAc.GetById(id);
            var edituser = new EditProfileViewModel { 
                Id=currentuser.Id,
                Name=currentuser.UserName,
                City=currentuser.City,
                Street=currentuser.Street,
                Country=currentuser.Country,
                ProfileImageUrl=currentuser.ProfileImg,
                PhoneNumber=currentuser.PhoneNumber
                };
            return View(edituser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
                return View(model);
            var ImgUrl=model.ProfileImageUrl;
            if(model.File!=null)
            {
                if (ImgUrl != null)
                {
                    var result = await _photo.DeletePhotoAsync(ImgUrl);
                }
                var addresult = await _photo.AddPhotoAsync(model.File);
                ImgUrl = addresult.Url.ToString();
            }
            var currentuserId = _contextAccessor.HttpContext.User.GetUserId();
            var currentuser = await _repoAc.GetById(currentuserId);
            currentuser.UserName = model.Name;
            currentuser.City = model.City;
            currentuser.Street = model.Street;
            currentuser.Country=model.Country;
            currentuser.PhoneNumber = model.PhoneNumber;
            currentuser.ProfileImg = ImgUrl;
            
            _repoAc.Update(currentuser);
            return RedirectToAction("Profile","Account");
        }

        public async Task<IActionResult> OrdersReceived()
        {
            return View();
        }

        public async Task<IActionResult> OrderReceivedById()
        {
            var currentuser = _contextAccessor.HttpContext.User.GetUserId();
            var results = await _receivedOrder.GetAllOrderBySeller(currentuser);
            return View(results);
        }

        public async Task<IActionResult> receiveditemsbyOrder(int id)
        {
            var results = await _receiveditems.GetByOrder(id);
            return View(results);
        }
        public async Task<IActionResult> OrdersPlaced()
        {
            var currentuserid = _contextAccessor.HttpContext.User.GetUserId();
            var results = await _placedorderitems.GetAll();
            return View(results);
        }
        public async Task<IActionResult> OrderPlacedById(int id)
        {
            var curresntuser = _contextAccessor.HttpContext.User.GetUserId();
            var result= await _Orderplaced.GetAllByAppUserId(curresntuser);
            return View(result);
        }

        public async Task<IActionResult> placeditemsbyOrder(int id)
        {
            var results = await _placedorderitems.GetByOrderId(id);
            return View(results);
        }

    }
}
