using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AuthService.Middlewares;
/// <summary>
/// Handles database exceptions
/// </summary>
public class DatabaseExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// Tries to handle a database exception
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is DbUpdateException dbEx && dbEx.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
        {
            var problem = new ProblemDetails
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Detail = "A unique constraint was violated."
            };
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            problem.Extensions["traceId"] = traceId;
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true;
        }

        return false;
    }
}