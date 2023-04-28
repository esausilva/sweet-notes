namespace Api.DI;

public static class ApiDependencyConfigurationExtensions
{
    public static ConfigurationManager GetApiConfigurations(this ConfigurationManager configuration)
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory());
        configuration.AddJsonFile("appsettings.json");
        
        Console.WriteLine(configuration.GetDebugView()); // Useful for troubleshooting multiple configuration hierarchies
        
        return configuration;
    }
}