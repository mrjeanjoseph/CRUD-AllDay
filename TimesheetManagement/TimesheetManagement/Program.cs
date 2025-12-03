using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TimesheetManagement.Infrastructure;
using TimesheetManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure (EF Core DbContext, repositories, UoW)
builder.Services.AddInfrastructure(builder.Configuration);

// Add NotificationSender
builder.Services.AddScoped<TimesheetManagement.Application.Common.Abstractions.INotificationSender, NotificationSender>();

// Application Insights for ASP.NET Core
builder.Services.AddApplicationInsightsTelemetry();

// MVC + views
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();