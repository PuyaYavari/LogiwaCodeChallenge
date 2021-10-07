using Merchandising.Enums;
using Merchandising.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Merchandising.Middlewares
{
	public class ErrorHandlerMiddleware
	{
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case MerchandisingException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await response.WriteAsync(JsonSerializer.Serialize(new { code = ((MerchandisingException)error).code, message = error?.Message }));
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await response.WriteAsync(JsonSerializer.Serialize(new { code = EnumErrors.UNKNOWN, message = error?.Message }));
                        break;
                }

                
            }
        }
    }
}
