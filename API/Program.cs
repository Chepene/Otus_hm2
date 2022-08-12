using Core.DB;
using Microsoft.EntityFrameworkCore;
using Npgsql;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        //builder.Configuration
        //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //.AddEnvironmentVariables();
 
        var dbConnectionString = builder.Configuration.GetConnectionString("DbConnection");
        Console.WriteLine("CONNECTIONSTRING: " + dbConnectionString);
    

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DataContext>(opt =>
        {
            opt.UseNpgsql(dbConnectionString);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}