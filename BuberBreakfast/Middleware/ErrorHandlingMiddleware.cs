using System.Net;
using System.Text.Json;
using BuberBreakfast.Models;

namespace BuberBreakfast.Middleware;

class ErrorHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ErrorHandlingMiddleware> _logger;

  public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(httpContext, ex);
    }
  }

  private async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";
    var response = context.Response;

    var errorResponse = new ErrorResponse(
      success: false,
      message: ""
    );

    switch (exception)
    {
      case KeyNotFoundException ex:
        response.StatusCode = (int)HttpStatusCode.NotFound;
        errorResponse.Message = ex.Message;
        break;
      default:
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        errorResponse.Message = "Internal Server errors. Check Logs!";
        break;
    }

    _logger.LogError(exception.Message);
    var result = JsonSerializer.Serialize(errorResponse);
    Console.WriteLine(result);
    await context.Response.WriteAsync(result);
  }
}
