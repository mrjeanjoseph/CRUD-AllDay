using CCMS.DomainService.DataAccess;
using CCMS.DomainService.UserData;
using CCMS.DomainServiceAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("CCMSConn") ??
    throw new InvalidOperationException("Connection string 'CCMSConn' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Personal Services
builder.Services.AddTransient<IInventoryData, InventoryData>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IProductData, ProductData>();
builder.Services.AddTransient<ISaleData, SaleData>();
builder.Services.AddTransient<IUserData, UserData>();

var secretKey = builder.Configuration.GetValue<string>("Secrets:SecurityKey");
builder.Services.AddAuthentication(options =>
{
    //options.DefaultAuthenticateScheme = "JwtBearer";
    //options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Updated to use JwtBearerDefaults
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Updated to use JwtBearerDefaults
})
    .AddJwtBearer(jwtBearerOptions => // Updated to use JwtBearerDefaults
    {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? "")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Clean Crew Management Services",
            Version = "v1",
        }
    );
});

var app = builder.Build();

ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

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

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "CCMS");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
