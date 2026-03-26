namespace HelsiPrototype.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        } catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                UnauthorizedAccessException => 403,
                KeyNotFoundException => 404,
                _ => 400
            };

            var response = new
            {
                success = false,
                message = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
