using BlinkIt.Data;
using BlinkIt.Helpers;
using BlinkIt.Models;
using BlinkIt.Repository.Implements;
using BlinkIt.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IOrderReceivedRepo,OrderReceivedRepo>();
builder.Services.AddScoped<IOrderreceiveditemsRepo,OrderReceivedItemsRepo>();
builder.Services.AddScoped<IOrderPlacedItemsRepo, OrderPlacedItemsRepo>();
builder.Services.AddScoped<IOrderPlacedRepo, OrderPlacedRepo>();
builder.Services.AddScoped<IShoppingCartItemsRepo, ShoppingCartItemsRepo>();
builder.Services.AddScoped<IShoppingCartRepo,ShoppingCartRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IProductRepo,ProductRepo>();
builder.Services.AddScoped<ICategoryRepo,CategoryRepo>();
builder.Services.AddScoped<ISubCategoryRepo, SubCategoryRepo>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySetting"));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Dbconnection")
    ));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
await AppSeed.SeedUsersAndRolesAsync(app);
app.Run();
