﻿using ApplicationFormServer.Contracts;
using ApplicationFormServer.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ApplicationFormServer.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExeptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal server Error"
                        }.ToString());

                    }
                });
            });
        }
    }
}
