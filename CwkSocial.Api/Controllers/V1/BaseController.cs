﻿using System;
using CwkSocial.Api.Contracts.Common;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CwkSocial.Api.Controllers.V1
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleErrorResponse(List<Error> errors)
        {
            var apiError = new ErrorResponse();

            if (errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.FirstOrDefault(e => e.Code == ErrorCode.NotFound);

                apiError.StatusCode = 404;
                apiError.StatusPhrase = "Not Found";
                apiError.TimeStamp = DateTime.Now;
                apiError.Errors.Add(error.Message);

                return NotFound(apiError);
            }


            apiError.StatusCode = 500;
            apiError.StatusPhrase = "Internal Server Error";
            apiError.TimeStamp = DateTime.Now;
            apiError.Errors.Add("Unhandled Exception");

            return StatusCode(500, apiError);
            
        }
    }
}

