using Client_Home.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification;

using Client_Home.Models;
using Client_Home.Areas.Admin.DTO.Customers;
using Client_Home.Areas.Admin.DTO.Employees;
using Client_Home.Areas.Admin.DTO.Category;
using Client_Home.Areas.Admin.DTO.Product;
using Client_Home.Areas.Admin.DTO.ProductBatch;
using Client_Home.Areas.Admin.DTO.Suppliers;
using NuGet.Protocol.Core.Types;
using Client_Home.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SalesLeadEntity")));
builder.Services.AddDbContext<ConveniencestoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbCONVENIENCESTORE")));
builder.Services.AddScoped<IAddCusFromExcel, AddCusFromExcel>();
builder.Services.AddScoped<IAddEmployFromExcel, AddEmployFromExcel>();
builder.Services.AddScoped<IAddCategoryFromExcel, AddCategoryFromExcel>();
builder.Services.AddScoped<IAddProductFromExcel, AddProductFromExcel>();
builder.Services.AddScoped<IAddProductBatchFromExcel, AddProductBatchFromExcel>();
builder.Services.AddScoped<IAddSupplierFromExcel, AddSupplierFromExcel>();
builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
   .AddRoles<IdentityRole>()
   .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    //.AddRoles<IdentityRole>()
    //.AddEntityFrameworkStores<Client_Home.Data.ConveniencestoreContext>();

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;

});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();
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
app.UseSession();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "AdminRoles",
    areaName: "Admin",
    pattern: "{controller=AdminRoles}/{action=Index}/{id?}");
app.Run();