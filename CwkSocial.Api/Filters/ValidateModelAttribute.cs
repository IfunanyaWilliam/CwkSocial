using System;
using CwkSocial.Api.Contracts.Common;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CwkSocial.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse();
                apiError.StatusCode = 404;
                apiError.StatusPhrase = "Bad Request";
                apiError.TimeStamp = DateTime.Now;

                var errors = context.ModelState.AsEnumerable();

                foreach(var error in errors)
                {
                    foreach(var inner in error.Value.Errors)
                    {
                        apiError.Errors.Add(inner.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
                //TO DO: Make sure Asp.Net Core doesn't override our action body
            }
        }
    }
}

