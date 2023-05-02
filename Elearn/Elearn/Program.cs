using Elearn.Data;
using Elearn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); //(AddIdentity-oz model idenity olacaq birde rollari,AddEntityFrameworkStores-saxlanma yeri olacaq,AddDefaultTokenProviders-sessionda haslanmis datalar saxlamaq ucun )



builder.Services.Configure<IdentityOptions>(opt =>  //sertler geydiyat ucun
{
    opt.Password.RequiredLength = 8;  //mutleq 8 sayda olsun paswordd
    opt.Password.RequireDigit = true;  //mutleq regem olsun paswordda
    opt.Password.RequireLowercase = true; //pasword icinde kicik herf olsun
    opt.Password.RequireUppercase = true; //pasword icinde boyuk herf olsun
    opt.Password.RequireNonAlphanumeric = true; //herf ve regem olmayanda olsun(isareler)

    opt.User.RequireUniqueEmail = true;  //her userin oz emaili olmalidi(unique)

    opt.Lockout.MaxFailedAccessAttempts = 3; //3 defe tekrar tekrar sehv girse user block olsun
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30); //sehv giribse nece defe nece deqeden sonra cehd etsin
    opt.Lockout.AllowedForNewUsers = true; //teze geydiydan kecenler bir nece defe sehv ede bilsin block olmasin tezeler


});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
