using Microsoft.EntityFrameworkCore;
using AdventureWorks.Domain.DataAccessLayer;
using System;

namespace AdventureWorks.ServiceAPI;

public class Program {

    public static void Main(string[] args) {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        var connectionString = builder.Configuration.GetConnectionString("AdWConnStr") ?? 
            throw new InvalidOperationException("Connection string 'AdWConnStr' not found.");

        builder.Services.AddDbContext<AdWDbContext>(options =>
            options.UseSqlServer(connectionString));

        //builder.Services.AddDbContext<AdWDbContext>(options => options
        //.UseSqlServer(builder.Configuration.GetConnectionString("AdWConnStr")));

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
