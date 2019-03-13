using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Services.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ErrorHandlingMiddleware));

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch ( Exception error)
            {
                await HandleExceptionAsync(context, error);
                throw;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            _log.Error(error.Message, error);
            return Task.CompletedTask;
        }
    }
}
