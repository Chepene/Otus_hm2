var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

Console.WriteLine("Start Migrating..");

Thread.Sleep(30_000);

Console.WriteLine("Finished Migration");

