using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.ServiceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.ServiceAPI;

public class Program {

    public static void Main(string[] args) {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        //Connecting to the DB
        var connectionString = builder.Configuration.GetConnectionString("AdWConnStr") ??
            throw new InvalidOperationException("Connection string 'AdWConnStr' not found.");
        builder.Services.AddDbContext<AdWDbContext>(options =>
            options.UseSqlServer(connectionString));
        //builder.Services.AddDbContext<AdWDbContext>(options => options
        //.UseSqlServer(builder.Configuration.GetConnectionString("AdWConnStr")));

        // AutoMapper and AutoMapper.Extentions.Microsoft.DependencyInjection packages
        // must be in the same version -> Currently 12.0.0 for both
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
