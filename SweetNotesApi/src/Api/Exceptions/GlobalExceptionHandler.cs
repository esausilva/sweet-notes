using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Exceptions;

public static class GlobalExceptionHandler
{
    public static void Configure(IApplicationBuilder builder)
    {
        builder.Run(async context => 
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            await ProblemDetailsFactory(exceptionHandlerPathFeature).ExecuteAsync(context);
        });
    }
    
    private static IResult ProblemDetailsFactory(IExceptionHandlerPathFeature? exceptionHandlerPathFeature)
    {
        IResult problemResult;
        
        switch (exceptionHandlerPathFeature?.Error)
        {
            case UserAlreadyExistsException:
            {
                problemResult = Results.ValidationProblem
                (
                    type: "https://httpstatuses.com/409",
                    title: "User already exists.",
                    statusCode: StatusCodes.Status409Conflict,
                    errors: new Dictionary<string, string[]>
                    {
                        { "account_exists", ["Account already exists."] }
                    }
                );
                break;
            }
            case UnauthorizedException:
            {
                problemResult = Results.ValidationProblem
                (
                    type: "https://httpstatuses.com/401",
                    title: "User not authorized.",
                    statusCode: StatusCodes.Status401Unauthorized,
                    errors: new Dictionary<string, string[]>
                    {
                        { "not_authorized", ["Email or Password is incorrect."] }
                    }
                );
                break;
            }
            case NotFoundException:
            {
                var details = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/404",
                    Title = "Special Someone Not Found.",
                    Status = StatusCodes.Status404NotFound
                };

                problemResult = Results.Problem(details);
                break;
            }
            case CannotUpdateSamePasswordException:
            {
                problemResult = Results.ValidationProblem
                (
                    type: "https://httpstatuses.com/409",
                    title: "Same Password.",
                    statusCode: StatusCodes.Status409Conflict,
                    errors: new Dictionary<string, string[]>
                    {
                        { "same_password", ["Cannot update to the same password."] }
                    }
                );
                break;
            }
            case PasswordMismatchException:
            {
                problemResult = Results.ValidationProblem
                (
                    type: "https://httpstatuses.com/409",
                    title: "Password Mismatch.",
                    statusCode: StatusCodes.Status409Conflict,
                    errors: new Dictionary<string, string[]>
                    {
                        { "password_mismatch", ["The current password is incorrect."] }
                    }
                );
                break;
            }
            case ApiValidationException:
            {
                var exp = (ApiValidationException)exceptionHandlerPathFeature!.Error;
                
                problemResult = Results.ValidationProblem
                (
                    exp.Errors,
                    type: "https://httpstatuses.com/400",
                    statusCode: StatusCodes.Status400BadRequest
                );
                break;
            }
            default:
            {
                var details = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/500",
                    Title = "An error occurred while processing your request.",
                    Status = StatusCodes.Status500InternalServerError
                };
            
                problemResult = Results.Problem(details);
                break;
            }
        }

        return problemResult;
    }
}