using Api.GraphQLEndpoints.NoteEps;
using Api.GraphQLEndpoints.NoteEps.Types;
using Api.GraphQLEndpoints.SpecialSomeoneEps;
using Api.GraphQLEndpoints.SpecialSomeoneEps.Types;
using Api.GraphQLEndpoints.UserEps.Types;
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
            .AddTypeExtension<NoteQueries>()

            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<SpecialSomeoneMutations>()
            .AddTypeExtension<NoteMutations>()

            .AddType<SpecialSomeoneType>()
            .AddType<UserType>()
            .AddType<NoteType>()
            .AddType<CreateNoteInputType>()
            .AddType<CreateSpecialSomeoneInputType>()

            .AddFiltering()
            .AddSorting()

            .AddType<SpecialSomeoneFilterInputType>();

        // .AddGlobalObjectIdentification(); // TODO: Add Node
        
        return services;
    }
}