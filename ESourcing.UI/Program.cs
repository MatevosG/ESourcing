using ESourcing.Core.Entities;
using ESourcing.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();//.AddRazorRuntimeCompilation();
builder.Services.AddMvc();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<WebAppContext>(option =>
                option.UseNpgsql(
                    builder.Configuration
                        .GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<AppUser,IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<WebAppContext>();

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

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
