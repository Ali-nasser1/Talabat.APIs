﻿using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));
            #region Error handling
            Services.Configure<ApiBehaviorOptions>(Options =>
    {
        Options.InvalidModelStateResponseFactory = (actionContext) =>
        {

            var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                 .SelectMany(P => P.Value.Errors)
                                                 .Select(E => E.ErrorMessage)
                                                 .ToList();

            var ValidationErrorResponse = new ApiValidationErrorResponse()
            {
                Errors = errors
            };
            return new BadRequestObjectResult(ValidationErrorResponse);
        };
    }); 
            #endregion

            return Services;
        }
    }
}
