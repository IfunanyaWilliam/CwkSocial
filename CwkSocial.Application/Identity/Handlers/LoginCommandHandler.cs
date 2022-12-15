using System;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Identity.Commands;
using CwkSocial.Application.Models;
using CwkSocial.Application.Options;
using CwkSocial.Dal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CwkSocial.Application.Services;
using Cwk.Domain.Aggregates.UserProfileAggregate;

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

                var identityUser = await ValidateAndGetIdentityAsync(request, result);
                if (result.IsError) return result;

                var userProfile = await _ctx.UserProfiles
                                        .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);
              
                result.PayLoad = GetJwtTokenString(identityUser, userProfile);
                return result;
            }
            catch (Exception e)
            {
                result.AddError(ErrorCode.UnknownError, e.Message);
            }

            return result;
        }


        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginCommand request,
                            OperationResult<string> result)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Username);

            if (identityUser == null)
                result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessage.NonExistentIdentityUser);
            

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessage.IncorrectPassword);

            return identityUser;
        }


        private string GetJwtTokenString(IdentityUser identityUser, UserProfile userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                   new Claim("IdentityId", identityUser.Id),
                    new Claim("UserProfileId", userProfile.UserProfileId.ToString())
               });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }

    }
}

