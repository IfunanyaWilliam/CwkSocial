﻿using System;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Identity.Commands;
using CwkSocial.Application.Models;
using CwkSocial.Application.Options;
using CwkSocial.Dal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CwkSocial.Application.Services;

namespace CwkSocial.Application.Identity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<string>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;

        public LoginCommandHandler(DataContext ctx,
                                   UserManager<IdentityUser> userManger,
                                   IdentityService identityService)
        {
            _ctx                = ctx;
            _userManager        = userManger;
            _identityService    = identityService;
        }

        public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();

            try
            {

                var identityUser = await _userManager.FindByEmailAsync(request.Username);

                if (identityUser == null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.IdentityUserDoesNotExist,
                        Message = $"Unable to find a user with the specified username"
                    };
                    result.Errors.Add(error);

                    return result;
                }

                var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

                if (!validPassword)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.IncorrectPassword,
                        Message = $"The password provided is incorrect"
                    };
                    result.Errors.Add(error);

                    return result;
                }

                var userProfile = await _ctx.UserProfiles
                                        .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);

                var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim("IdentityId", identityUser.Id),
                    new Claim("UserProfileId", userProfile.UserProfileId.ToString())
                });
                    
                var token = _identityService.CreateSecurityToken(claimsIdentity);
                result.PayLoad = _identityService.WriteToken(token);
                return result;

            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Code = ErrorCode.UnknownError,
                    Message = $"{e.Message}"
                };

                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}

