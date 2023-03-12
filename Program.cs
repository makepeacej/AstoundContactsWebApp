using AstoundContactsWebApp.Data;
using AstoundContactsWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{

    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    string connStr;

    if (env == "Development")
    {
        connStr = builder.Configuration.GetConnectionString("DefaultConnection");


    }
    else
    {
        // Use connection string provided at runtime by Heroku.
        var connUrl = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");

        connUrl = connUrl.Replace("mysql://", string.Empty);
        var userPassSide = connUrl.Split("@")[0];
        var hostSide = connUrl.Split("@")[1];

        var connUser = userPassSide.Split(":")[0];
        var connPass = userPassSide.Split(":")[1];
        var connHost = hostSide.Split("/")[0];
        var connDb = hostSide.Split("/")[1].Split("?")[0];


        connStr = $"server={connHost};Uid={connUser};Pwd={connPass};Database={connDb}";


    }

    options.UseSqlServer(connStr);

});
//options.UseSqlServer(connectionString)

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedUser.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
