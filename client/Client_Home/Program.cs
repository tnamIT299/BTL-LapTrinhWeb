using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Client_Home.Areas.Admin.DTO.Customers;
using Client_Home.Areas.Admin.DTO.Employees;
using Client_Home.Areas.Admin.DTO.Category;
using Client_Home.Areas.Admin.DTO.Product;
using Client_Home.Areas.Admin.DTO.ProductBatch;
using Client_Home.Areas.Admin.DTO.Suppliers;
using Client_Home.Repository;
using Client_Home.Areas.Admin.Models;
using Client_Home.Areas.Admin.Services.SendEmail;
using Client_Home.Areas.Admin.Services;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System.Configuration;
using DocumentFormat.OpenXml.Math;
using Client_Home.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json;
using Client_Home.Areas.Admin.Services.Models;

var builder = WebApplication.CreateBuilder(args);
var ten = "";
var diaChi = "";
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
var services = builder.Services;

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
builder.Services.AddOptions();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;

});

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        // Đọc thông tin Authentication:Google từ appsettings.json
        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

        // Thiết lập ClientID và ClientSecret để truy cập API google
        googleOptions.ClientId = googleAuthNSection["ClientId"];
        googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
        googleOptions.CallbackPath = "/signin-google";

    })
    .AddFacebook(facebookOptions => {
        // Đọc cấu hình
        IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
        facebookOptions.AppId = facebookAuthNSection["AppId"];
        facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
        // Thiết lập đường dẫn Facebook chuyển hướng đến
        facebookOptions.CallbackPath = "/signin-facebook";
    });
//builder.Services.AddHostedService<ScheduledJob>();
//var smtpSettingsSection = builder.Configuration.GetSection("SmtpSettings");
//builder.Services.Configure<SmtpSettings>(smtpSettingsSection);
//services.AddSingleton<EmailService>();



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();
var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/Account/Login", "?statusCode={0}");
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    //endpoints.MapGet("/", async context => {
    //    await context.Response.WriteAsync("Hello World!");
    //});

    endpoints.MapPost("/testmail", async context =>
    {

        // Lấy dịch vụ sendmailservice
        var sendmailservice = context.RequestServices.GetService<IEmailService>();
        using (StreamReader reader = new StreamReader(context.Request.Body, System.Text.Encoding.UTF8))
        {
            var requestBody = await reader.ReadToEndAsync();
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

            // Lấy giá trị của trường "email"
            if (jsonData.TryGetValue("email", out var email))
            {
                MailContent content = new MailContent
                {
                   
                    From = "PH Mart",
                    To = email,
                    Subject = "PH Mart",
                    Body = @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600""
       style=""background-color:#ffffff;border:1px solid #dedede;border-radius:3px"">
    <tbody>
        <tr>
            <td align=""center"" valign=""top"">

                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""
                       style=""background-color:#96588a;color:#ffffff;border-bottom:0;font-weight:bold;line-height:100%;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;border-radius:3px 3px 0 0"">
                    <tbody>
                        <tr>
                            <td style=""padding:36px 48px;display:block"">
                                <h1 style=""font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:30px;font-weight:300;line-height:150%;margin:0;text-align:left;color:#ffffff;background-color:inherit"">
                                    Cảm ơn bạn đã đặt hàng
                                </h1>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </td>
        </tr>
        <tr>
            <td align=""center"" valign=""top"">

                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
                    <tbody>
                        <tr>
                            <td valign=""top"" style=""background-color:#ffffff"">

                                <table border=""0"" cellpadding=""20"" cellspacing=""0"" width=""100%"">
                                    <tbody>
                                        <tr>
                                            <td valign=""top"" style=""padding:48px 48px 32px"">
                                                <div style=""color:#636363;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:14px;line-height:150%;text-align:left"">

                                                    <p style=""margin:0 0 16px"">Xin chào {{TenKhachHang}},</p>


                                                    <h2 style=""color:#96588a;display:block;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:0 0 18px;text-align:left"">
                                                        [Đơn hàng] ({{NgayDat}})
                                                    </h2>

                                                    


                                                    <table cellspacing=""0"" cellpadding=""0"" border=""0""
                                                           style=""width:100%;vertical-align:top;margin-bottom:40px;padding:0"">
                                                        <tbody>
                                                            <tr>
                                                                <td valign=""top"" width=""50%""
                                                                    style=""text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;border:0;padding:0"">
                                                                    <h2 style=""color:#96588a;display:block;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:0 0 18px;text-align:left"">
                                                                        Thông tin người nhận
                                                                    </h2>

                                                                    <address style=""padding:12px;color:#636363;border:1px solid #e5e5e5"">
                                                                        {{TenKhachHang}}<br><br><a href=""""
                                                                                                                      style=""color:#96588a;font-weight:normal;text-decoration:underline""
                                                                                                                      target=""_blank""></a> <br><a href=""mailto:{{Email}}""
                                                                                                                                                           target=""_blank"">{{Email}}</a>
                                                                    </address>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <p style=""margin:0 0 16px"">
                                                        Chúng tôi đang tiến hành hoàn thiện đơn đặt hàng của bạn
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </td>
                        </tr>
                    </tbody>
                </table>

            </td>
        </tr>
    </tbody>
</table>"
                };
                string ngayDat = DateTime.Now.ToString("dd-MM-yyyy");
                content.Body = content.Body.Replace("{{Email}}", email);
                content.Body = content.Body.Replace("{{TenKhachHang}}", email);
                content.Body = content.Body.Replace("{{NgayDat}}", ngayDat);
                await sendmailservice.SendMail(content);
                await context.Response.WriteAsync("Send mail");

            }
            else
            {
                // Phản hồi lỗi nếu trường "email" không được cung cấp
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Email is required");
            }
        }
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "AdminRoles",
    areaName: "Admin",
    pattern: "{controller=AdminRoles}/{action=Index}/{id?}");
app.Run();