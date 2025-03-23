using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.ServiceAPI.Logging;
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
        builder.Services.AddDbContext<AdWDbContext>(options => options.UseSqlServer(connectionString));

        // Existing code
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddLogging(logging => {
        //    logging.AddAdWFileLogger(options => {
        //        builder.Configuration.GetSection("Logging").GetSection("AdwFileLogger").Bind(options);
        //    });
        //});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Configuration.GetSection("Logging").GetSection("AdwFileLogger").Bind(new AdWFileLoggerOptions());
        app.Run();        
    }
}
