using Core.DB;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API.Controllers;

public class WeatherForecastController : BasicApiController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly DataContext _context;  
    private readonly IConfiguration _configuration;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogDebug("GetWeatherForecast");

        return new List<WeatherForecast>(){
            new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = TestDB(GetConnectionString(_configuration))
            },
            new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = GetConnectionString(_configuration)
            },

        };

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private string GetConnectionString(IConfiguration config)
    {
        var host = _configuration.GetValue<string>("DBHOSTNAME");
        var port = _configuration.GetValue<string>("DBPORT");
        var dbName = _configuration.GetValue<string>("DBNAME");
        var username = _configuration.GetValue<string>("DBUSERNAME");
        var password = _configuration.GetValue<string>("DBPASSWORD");      

        return $"Host={host}:{port};Database={dbName};Username={username};Password={password}";
    }

    private string TestDB(string dbConnectionString)
    {
        try
        {
            using (var connection = new NpgsqlConnection(dbConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand("select 1", connection))
                    {
                        var t = command.ExecuteNonQuery();
                    }
                    connection.Close();

                    return "Ok";
                }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            Console.WriteLine(ex.Message);

            return ex.Message;
        }

        
    }
}
