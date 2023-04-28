using Api.DI;
using Data.DI;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.GetApiConfigurations();

var services = builder.Services;
services.ConfigureDataDependencies(configuration);

var app = builder.Build();
app.MapGraphQL();
app.MapGet("/", () => "Hello World!");
app.Run();
