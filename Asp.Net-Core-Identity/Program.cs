using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Asp.Net_Core_Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Asp.Net_Core_Identity.CustomValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppIdentityDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppIdentityDbContext)).GetName().Name);
    });
});


builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddDefaultTokenProviders();
CookieBuilder cookieBuilder = new();
cookieBuilder.Name = "MyIdentityProject";
cookieBuilder.HttpOnly = false;
cookieBuilder.SameSite = SameSiteMode.Lax;
cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = new PathString("/Home/Login/");
    opts.Cookie = cookieBuilder;
    opts.SlidingExpiration = true;
    opts.ExpireTimeSpan = TimeSpan.FromDays(7);
    opts.AccessDeniedPath = new PathString("/Member/AccesDenied/");

});


builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseAuthentication();
app.UseMvcWithDefaultRoute();

app.Run();

