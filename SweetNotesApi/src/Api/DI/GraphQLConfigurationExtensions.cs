using Api.GraphQLEndpoints.SpecialSomeone;
using Data.Config;

namespace Api.DI;

public static class GraphQLConfigurationExtensions
{
    public static IServiceCollection ConfigureGraphQLDependencies
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddGraphQLServer()
            .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)

            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<SpecialSomeoneQueries>()

            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<SpecialSomeoneMutations>();

            // .AddGlobalObjectIdentification();
        
        return services;
    }
}