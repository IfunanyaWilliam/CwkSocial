using System;
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
            _keys.ForEach(k =>
            {

            });

            if (!context.ActionArguments.TryGetValue(_key, out var value)) return;
            if (Guid.TryParse(value?.ToString(), out var guid)) return;

            var apiError = new ErrorResponse
            {
                StatusCode = 400,
                StatusPhrase = "Bad Request",
                TimeStamp = DateTime.Now
            };

            apiError.Errors.Add($"The identifier for {_key} is not a correct GUID format");
            context.Result = new ObjectResult(apiError);
        }
    }
}

