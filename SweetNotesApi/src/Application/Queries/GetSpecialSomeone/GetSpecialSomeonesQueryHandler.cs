// using Data.Config;
// using Microsoft.EntityFrameworkCore;
//
// namespace Application.Queries.GetSpecialSomeone;
//
// public class GetSpecialSomeonesQueryHandler : IQueryRequest<GetSpecialSomeoneQuery, List<Domain.Entities.SpecialSomeone>>
// {
//     private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
//
//     public GetSpecialSomeonesQueryHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
//     {
//         _dbContextFactory = dbContextFactory;
//     }
//
//     async Task<List<Domain.Entities.SpecialSomeone>> IQueryRequest<GetSpecialSomeoneQuery, List<Domain.Entities.SpecialSomeone>>
//         .Handle(GetSpecialSomeoneQuery query, CancellationToken cancellationToken)
//     {
//         await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
//
//         return await dbContext.SpecialSomeone
//             .Where(x => x.UserId == query.UserId)
//             .Include(x => x.User)
//             .ToListAsync(cancellationToken);
//     }
// }