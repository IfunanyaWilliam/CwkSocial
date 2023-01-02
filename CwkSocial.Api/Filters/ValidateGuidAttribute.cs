﻿using System;
using CwkSocial.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CwkSocial.Api.Filters
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly List<string> _keys;

        public ValidateGuidAttribute(string key)
        {
            _keys = new List<string>();
            _keys.Add(key);
        }

        public ValidateGuidAttribute(string key1, string key2)
        {
            _keys = new List<string>();
            _keys.Add(key1);
            _keys.Add(key2);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasError = false;
            var apiError = new ErrorResponse();
            _keys.ForEach(key =>
            {
                if (!context.ActionArguments.TryGetValue(key, out var value)) return;
                if (!Guid.TryParse(value?.ToString(), out var guid))
                {
                    hasError = true;
                    apiError.Errors.Add($"The identifier for {key} is not a correct GUID format");
                }
                
            });

            if (hasError)
            {
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad Request";
                apiError.TimeStamp = DateTime.Now;
                context.Result = new ObjectResult(apiError);
            }
        }
    }
}

