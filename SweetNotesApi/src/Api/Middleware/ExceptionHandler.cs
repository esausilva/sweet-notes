using Api.Exceptions;
using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middleware;

public static class ExceptionHandler
{
    public static WebApplication ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(handler => handler.Run(
            async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                await ProblemDetailsFactory(exceptionHandlerPathFeature).ExecuteAsync(context);
            })
        );
        
        return app;
    }

    private static IResult ProblemDetailsFactory(IExceptionHandlerPathFeature? exceptionHandlerPathFeature)
    {
        IResult problemResult;
        
        switch (exceptionHandlerPathFeature?.Error)
        {
            case UserAlreadyExistsException:
            {
                var details = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/409",
                    Title = "User already exists.",
                    Status = StatusCodes.Status409Conflict
                };

                problemResult = Results.Problem(details);
                break;
            }
            case UnauthorizedException:
            {
                var details = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "User not authorized.",
                    Status = StatusCodes.Status401Unauthorized
                };

                problemResult = Results.Problem(details);
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