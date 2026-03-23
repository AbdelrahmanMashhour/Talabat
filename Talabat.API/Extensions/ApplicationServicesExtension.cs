using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs.Helpers;
using Talabat.API.Errors;
using Talabat.API.Middlewares;
using Talabat.Core.Repositories.Contracts;
using Talabat.Repository;

namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ExceptionMiddleware>();
            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(m=>m.AddProfile(new MappingProfile));
            #region Configuration Of BadRequest Response (this if found validation request error)
            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (ActionContext context) =>
                {
                    var errors = context.ModelState.Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            #endregion

            return services;
        }
    }
}
