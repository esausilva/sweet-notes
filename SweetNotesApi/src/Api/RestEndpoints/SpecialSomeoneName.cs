using Api.Exceptions;
using Api.RestEndpoints.Models;
using Application.Queries;
using Application.Queries.GetSpecialSomeone;
using Domain.Entities;
using FluentValidation;

namespace Api.RestEndpoints;

public static class SpecialSomeoneName
{
    public static async Task<IResult> GetAsync
    (
        IValidator<SpecialSomeoneIdentifierRequest> validator,
        [AsParameters] SpecialSomeoneIdentifierRequest request,
        IQueryRequest<SpecialSomeoneNameQuery, SpecialSomeone?> queryRequest,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid is false)
            throw new ApiValidationException(validationResult.ToDictionary());
        
        var query = new SpecialSomeoneNameQuery(request.Identifier);
        var specialSomeone = await queryRequest.Handle(query, cancellationToken);
        
        if (specialSomeone is null)
            throw new NotFoundException();
        
        var response = new SpecialSomeoneNameResponse(
            specialSomeone.FirstName, 
            specialSomeone.LastName,
            specialSomeone.Nickname
        );
        
        return Results.Ok(response);
    }
}