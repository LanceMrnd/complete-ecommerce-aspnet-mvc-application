using eTickets.Models;
using eTickets.Models.Data;
using eTickets.Models.Data.Cart;
using eTickets.Models.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;




// DbContext configuration

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"))
);

builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));


//Authentication

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddSession();

//Json Serializer
builder.Services.AddControllersWithViews()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



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
app.UseSession();

//Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();




