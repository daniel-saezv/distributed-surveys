using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuthService.Utils;

/// <summary>
/// Provides utility methods for generating standardized problem results.
/// </summary>
public static class ProblemResults
{
    /// <summary>
    /// Generates a 403 Forbidden result with the specified detail message.
    /// </summary>
    /// <param name="detail"></param>
    /// <returns></returns>
    public static ProblemHttpResult AlreadyRegistered(string detail = "Operation could not be completed.") =>
        TypedResults.Problem(
            detail: detail,
            statusCode: StatusCodes.Status409Conflict,
            title: "Conflict"
        );
}