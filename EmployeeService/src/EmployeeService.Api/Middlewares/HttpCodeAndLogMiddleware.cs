using EmployeeService.Core.Exception;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;
using System.Text;

namespace EmployeeService.Api.Middlewares
{
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpCodeAndLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpCodeAndLogMiddleware>();
        }
    }
    public class HttpCodeAndLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpCodeAndLogMiddleware> _logger;

        public HttpCodeAndLogMiddleware(RequestDelegate next, ILogger<HttpCodeAndLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context == null)
            {
                return;
            }    

            try
            {
                context.Request.EnableBuffering();
                await _next(context);
            }
            catch(Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch(ex)
                {
                    case ApiException e:
                        // Custom application error
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await WriteAndLogResponeAsync(ex, context, HttpStatusCode.BadRequest, LogLevel.Error, $"BadRequest Exception !{ e.Message}");
                        break;
                    case NotFoundException e:
                        // Not found error
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await WriteAndLogResponeAsync(ex, context, HttpStatusCode.NotFound, LogLevel.Error, $"Not Found!{ e.Message}");
                        break;
                    case ValidationException e:
                        // Validation error
                        context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        await WriteAndLogResponeAsync(ex, context, HttpStatusCode.UnprocessableEntity, LogLevel.Error, $"Validation Exception!{ e.Message}");
                        break;
                    case AuthenticationException e:
                        // Validation error
                        context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        await WriteAndLogResponeAsync(ex, context, HttpStatusCode.UnprocessableEntity, LogLevel.Error, $"Validation Exception!{ e.Message}");
                        break;
                    default:
                        // Unhandled error
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await WriteAndLogResponeAsync(ex, context, HttpStatusCode.InternalServerError, LogLevel.Error, $"Server Error");
                        break;

                }
            }
        }
        
        private async Task WriteAndLogResponeAsync(
            Exception exception, 
            HttpContext httpContext, 
            HttpStatusCode httpStatusCode, 
            LogLevel logLevel, 
            string alternateMessage = null)
        {
            string requestBody = string.Empty;
            if(httpContext.Request.Body.CanSeek)
            {
                httpContext.Request.Body.Seek(0, System.IO.SeekOrigin.Begin);
                using (var sr = new System.IO.StreamReader(httpContext.Request.Body))
                {
                    requestBody = JsonConvert.SerializeObject(sr.ReadToEndAsync());
                }    
            }

            StringValues authorization;
            httpContext.Request.Headers.TryGetValue("Authorization", out authorization);

            var customDetails = new StringBuilder();
            customDetails
                .AppendFormat("\n  Service URL    :").Append(httpContext.Request.Path.ToString())
                .AppendFormat("\n  Request Method :").Append(httpContext.Request?.Method)
                .AppendFormat("\n  Request Body   :").Append(requestBody)
                .AppendFormat("\n  Authorization  :").Append(authorization)
                .AppendFormat("\n  Content-Type   :").Append(httpContext.Request?.Headers["Content-Type"].ToString())
                .AppendFormat("\n  Cookie         :").Append(httpContext.Request?.Headers["Cookie"].ToString())
                .AppendFormat("\n  Host           :").Append(httpContext.Request?.Headers["Host"].ToString())
                .AppendFormat("\n  Referer        :").Append(httpContext.Request?.Headers["Referer"].ToString())
                .AppendFormat("\n  Origin         :").Append(httpContext.Request?.Headers["Origin"].ToString())
                .AppendFormat("\n  User-Agent     :").Append(httpContext.Request?.Headers["User-Agent"].ToString())
                .AppendFormat("\n  ErrorMessage   :").Append(exception.Message);

            _logger.Log(logLevel, exception, customDetails.ToString());

            if(httpContext.Response.HasStarted)
            {
                _logger.LogError("The respone has already started, the http status code middleware will not be excuted.");
                return;
            }

            string responeMessage = JsonConvert.SerializeObject(
                new
                {
                    Message = string.IsNullOrWhiteSpace(exception.Message) ? alternateMessage : exception.Message,
                });

            httpContext.Response.Clear();
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = (int)httpStatusCode;
            await httpContext.Response.WriteAsync(responeMessage, Encoding.UTF8);

        }
    }
}
