using System;
using System.Net;
using System.Text;
using KiraNet.Camellia.Shared;
using Service.ApiService.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Service.ApiService.Extensions
{
    public static class ApplicationBuilderExtensions
    {
            public static void UseExceptionHandlingMiddleware(this IApplicationBuilder app)
            {
                app.UseExceptionHandler(options =>
                {
                    options.UseCors(ServiceConfiguration.Configs.AuthName);
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "application/json";
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                var err = new ErrorViewModel
                                {
                                    StackTrace = ex.Error.StackTrace,
                                    Message = ex.Error.Message,
                                    InnerMessage = GetInnerMessage(ex.Error)
                                };
                                var errStr = JsonConvert.SerializeObject(err, Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                    });
                                var bytes = Encoding.UTF8.GetBytes(errStr);
                                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                            }
                        });
                });
            }

            private static string GetInnerMessage(Exception ex)
            {
                if (ex == null) return null;
                return ex.InnerException != null ? GetInnerMessage(ex.InnerException) : ex.Message;
            }
    }
}
