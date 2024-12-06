using DataLibrary.Models.Account;
using MedTrackPro.Data;
using MedTrackPro.Hubs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DNTCaptcha.Core;
using MedTrackPro.UtilityMethods;


//https://www.youtube.com/watch?v=ScaOFvMn3Ek
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddSignalR();

builder.Services.AddDNTCaptcha(options =>
{
    options.UseCookieStorageProvider(SameSiteMode.Strict);
    options.EncryptionKey = "F2022266205d2@m2EfjIV7t@LZb";
});

builder.Services.AddScoped<UtilsMethods>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ClientHub>("/hub/client");

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var utilsMethod = services.GetRequiredService<UtilsMethods>();
    await utilsMethod.CreateRolesAndAdminAsync();
}

app.Run();
