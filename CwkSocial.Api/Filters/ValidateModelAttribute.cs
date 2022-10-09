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
        public override void OnActionExecuted(ActionExecutedContext context)
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
                    apiError.Errors.Add(error.Value.ToString());
                }

                context.Result = new JsonResult(apiError) { StatusCode = 400 };
                //TO DO: Make sure Asp.Net Core doesn't override our action body
            }
        }
    }
}

