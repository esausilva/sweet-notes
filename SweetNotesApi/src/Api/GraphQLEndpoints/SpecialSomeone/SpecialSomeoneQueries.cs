namespace Api.GraphQLEndpoints.SpecialSomeone;

[ExtendObjectType(Name = "Query")]
public class SpecialSomeoneQueries
{
    public async Task<IEnumerable<Domain.Entities.SpecialSomeone>> GetSpecialSomeonesByUserId
    (
        int userId,
        CancellationToken cancellationToken
    )
    {
        
        return new List<Domain.Entities.SpecialSomeone>();
    }
}