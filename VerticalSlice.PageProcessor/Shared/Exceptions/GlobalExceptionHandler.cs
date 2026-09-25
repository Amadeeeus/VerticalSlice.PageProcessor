using Microsoft.AspNetCore.Diagnostics;
using VerticalSlice.PageProcessor.Features.ProcessPage;
using VerticalSlice.PageProcessor.Features.ProcessPage.Constants;
using VerticalSlice.PageProcessor.Features.ProcessPage.Exceptions;

namespace VerticalSlice.PageProcessor.Shared.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger):IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProcessPageResponse response;

        if (exception is ProcessPageException pageException)
        {
            response = new ProcessPageResponse
            {
                IsError = 1,
                ErrorCode = pageException.ErrorCode,
                ErrorMessage = pageException.Message
            };
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else
        {
            logger.LogError(exception,"Unhandled exception");

            response = new ProcessPageResponse
            {
                IsError = 1,
                ErrorCode = ErrorCodes.InternalError,
                ErrorMessage = exception.Message
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}