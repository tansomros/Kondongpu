using Microsoft.AspNetCore.Mvc;
using Kondongpu.Application.Exceptions;
using KondongpuUnauthorizedAccessException = Kondongpu.Application.Exceptions.KondongpuUnauthorizedAccessException;
using NotFoundException = Kondongpu.Application.Exceptions.NotFoundException;
using ValidationException = Kondongpu.Application.Exceptions.ValidationException;

namespace Kondongpu.Presentation.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _jsonContentType = "application/json";

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            if (ex is ValidationException validationException)
            {
                var result = new ValidationProblemDetails(validationException.Errors)
                {
                    Detail = validationException.Message,
                    Status = StatusCodes.Status400BadRequest
                };

                httpContext.Response.ContentType = _jsonContentType;
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(result, CancellationToken.None);
            }
            else if (ex is KondongpuUnauthorizedAccessException unauthorizedAccessException)
            {
                var result = new ProblemDetails()
                {
                    Title = "ระบบไม่อนุญาตให้เข้าใช้งาน",
                    Detail = unauthorizedAccessException.Message,
                    Status = StatusCodes.Status401Unauthorized
                };

                httpContext.Response.ContentType = _jsonContentType;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsJsonAsync(result, CancellationToken.None);
            }
            else if (ex is KondongpuForbiddenAccessException forbiddenAccessException)
            {
                var result = new ProblemDetails()
                {
                    Title = "ไม่มีสิทธิในการเข้าใช้งาน",
                    Detail = forbiddenAccessException.Message,
                    Status = StatusCodes.Status403Forbidden,
                };

                httpContext.Response.ContentType = _jsonContentType;
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await httpContext.Response.WriteAsJsonAsync(result, CancellationToken.None);
            }
            else if (ex is NotFoundException notFoundException)
            {
                var result = new ProblemDetails()
                {
                    Title = "ไม่พบข้อมูล, หรือเส้นทางที่ระบุมา",
                    Detail = notFoundException.Message,
                    Status = StatusCodes.Status404NotFound
                };

                httpContext.Response.ContentType = _jsonContentType;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsJsonAsync(result, CancellationToken.None);
            }
            else if (ex is CreateKondongpuException createKondongpuException)
            {
                var result = new ProblemDetails()
                {
                    Title = "การออกคิวเกิดข้อผิดพลาด",
                    Detail = createKondongpuException.Message,
                    Status = StatusCodes.Status400BadRequest
                };

                httpContext.Response.ContentType = _jsonContentType;
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(result, CancellationToken.None);
            }
            else
            {
                var result = new ProblemDetails
                {
                    Title = "ระบบขัดข้อง",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                };

                httpContext.Response.ContentType = _jsonContentType;
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(result, CancellationToken.None);
            }
        }
    }
}
