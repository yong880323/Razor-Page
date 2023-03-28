using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Razor_PagesMovie.Models;
using Razor_PagesMovie.Util;

var builder = WebApplication.CreateBuilder(args);
//�K�[ Razor Pages �A��
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Razor_PagesMovieContext")));
//builder.Services.AddScoped<ITest, User>();

// �[�J�������ҪA��
builder.Services.AddAuthentication(a => a.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
    option.LoginPath = "/Account/index";
    option.LogoutPath = "/Util/Accountaction";
    //option.ExpireTimeSpan = TimeSpan.FromSeconds(30);
    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.AddScoped<Accountaction>();
builder.Services.AddScoped<User>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Razor_PagesMovie.Util.Accountaction>();

/*builder.Services.AddScoped(a =>
{
    a.IdleTimeout = TimeSpan.FromSeconds(10);
     = TimeSpan.FromSeconds(10);
});*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

// �����{�Ҥ�����
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
