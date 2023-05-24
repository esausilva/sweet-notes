using Api.DI;
using Api.Middleware;
using Api.RestEndpoints;
using Application.DI;
using Data.DI;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.GetApiConfigurations();

var services = builder.Services;
services
    .ConfigureApplicationDependencies(configuration)
    .ConfigureDataDependencies(configuration);

var app = builder.Build();
app.ConfigureExceptionHandler();
app.MapGraphQL();
app.MapUserEndpoint();
app.Run();
