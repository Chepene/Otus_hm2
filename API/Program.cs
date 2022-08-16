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

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DataContext>(opt =>
        {
            var connectionString = GetConnectionString(builder.Configuration);
            opt.UseNpgsql(connectionString);
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
    
    private static string GetConnectionString(IConfiguration config)
    {
        var host = config.GetValue<string>("DBHOSTNAME");
        var port = config.GetValue<string>("DBPORT");
        var dbName = config.GetValue<string>("DBNAME");
        var username = config.GetValue<string>("DBUSERNAME");
        var password = config.GetValue<string>("DBPASSWORD");      

        return $"Host={host}:{port};Database={dbName};Username={username};Password={password}";
    }
}