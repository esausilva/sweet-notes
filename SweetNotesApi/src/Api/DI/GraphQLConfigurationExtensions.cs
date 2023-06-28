using Api.GraphQLEndpoints.SpecialSomeone;
using Api.GraphQLEndpoints.User;
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
            .AddFairyBread()
            .AddAuthorization()
            .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)

            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<SpecialSomeoneQueries>()

            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<SpecialSomeoneMutations>()

            .AddType<SpecialSomeoneType>()
            .AddType<UserType>();

            // .AddGlobalObjectIdentification(); // TODO: Add Node
        
        return services;
    }
}