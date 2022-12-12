using ESourcing.Core.Entities;
using ESourcing.Core.Repositories;
using ESourcing.Core.Repositories.Base;
using ESourcing.Infrastructure.Data;
using ESourcing.Infrastructure.Repositories;
using ESourcing.Infrastructure.Repositories.Base;
using ESourcing.UI;
using ESourcing.UI.Clients;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<WebAppContext>(option =>
                option.UseNpgsql(
                    builder.Configuration
                        .GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 4;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireDigit = false;
})
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<WebAppContext>();


builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();//.AddRazorRuntimeCompilation();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "My Coockie";
            options.LoginPath = "Home/Login";
            options.LogoutPath = "Home/Logout";
            options.ExpireTimeSpan = TimeSpan.FromDays(3);
            options.SlidingExpiration = false;
        });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Home/Login";
    options.LogoutPath = $"/Home/Logout";
});

builder.Services.AddHttpClient();

builder.Services.AddHttpClient<ProductClient>();
builder.Services.AddHttpClient<AuctionClient>();
builder.Services.AddHttpClient<BidClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

//app.CreateAndSeedDatabase().Run();
app.Run();
