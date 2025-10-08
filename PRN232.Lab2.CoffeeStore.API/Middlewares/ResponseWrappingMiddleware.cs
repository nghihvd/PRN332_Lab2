using System.Text;
using System.Text.Json;

namespace PRN232.Lab2.CoffeeStore.API.Middlewares
{
    public class ResponseWrappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                // If 401/403 with empty body, return a standard JSON error envelope
                if ((context.Response.StatusCode == StatusCodes.Status401Unauthorized || context.Response.StatusCode == StatusCodes.Status403Forbidden)
                    && string.IsNullOrWhiteSpace(responseText))
                {
                    context.Response.Body = originalBodyStream;
                    context.Response.ContentType = "application/json";
                    var wrapper401 = new { success = false, data = (object?)null, error = new { message = context.Response.StatusCode == 401 ? "Unauthorized" : "Forbidden" } };
                    var json401 = JsonSerializer.Serialize(wrapper401, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    await context.Response.WriteAsync(json401);
                    return;
                }

                // Bypass non-JSON or empty responses
                var contentType = context.Response.ContentType ?? string.Empty;
                if (string.IsNullOrWhiteSpace(responseText) || !contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    await responseBody.CopyToAsync(originalBodyStream);
                    return;
                }

                // If already wrapped (has top-level 'success'), pass through
                var isAlreadyWrapped = responseText.TrimStart().StartsWith("{\"success\"", StringComparison.Ordinal);
                if (isAlreadyWrapped)
                {
                    await responseBody.CopyToAsync(originalBodyStream);
                    return;
                }

                object payload;
                try
                {
                    payload = JsonSerializer.Deserialize<object>(responseText, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })!;
                }
                catch
                {
                    payload = responseText;
                }

                var isSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;
                var wrapper = new
                {
                    success = isSuccess,
                    data = isSuccess ? payload : (object?)null,
                    error = isSuccess ? (object?)null : payload
                };

                var wrappedJson = JsonSerializer.Serialize(wrapper, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var bytes = Encoding.UTF8.GetBytes(wrappedJson);
                context.Response.ContentLength = bytes.Length;
                context.Response.Body = originalBodyStream;
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                // Uniform unhandled error shape
                context.Response.Body = originalBodyStream;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var error = new { success = false, error = new { message = "Internal server error", detail = ex.Message } };
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}


