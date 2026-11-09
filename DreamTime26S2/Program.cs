using DreamTime26S2.Data;
using DreamTime26S2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add EF database support using DefaultConnection connection string in appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Identity support using our custom ApplicationUser class
builder.Services.AddDefaultIdentity<ApplicationUser>(
    options => {
        options.SignIn.RequireConfirmedAccount = true;

        // Change password complexity requirements (policy)
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        //options.Password.RequireLowercase = true;
        //options.Password.RequireUppercase = false;
        //options.Password.RequireDigit = false;
    })
    .AddRoles<IdentityRole>()  // Add support for roles
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add other services (dependencies)
builder.Services.AddControllersWithViews();

// Build the web app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
