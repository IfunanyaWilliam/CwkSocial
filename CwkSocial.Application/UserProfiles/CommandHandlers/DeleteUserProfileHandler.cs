using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfile, OperationResult<UserProfile>>
    {

        private readonly DataContext _ctx;

        public DeleteUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;
        }


        public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfile request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();
            var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            if (userProfile is null)
            {
                result.IsError = true;
                var error = new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"No UserProfile with ID: {request.UserProfileId} found"
                };
                result.Errors.Add(error);

                return result;
            }

            _ctx.UserProfiles.Remove(userProfile);
            await _ctx.SaveChangesAsync();

            result.PayLoad = userProfile;
            return result;
        }

    }
}

