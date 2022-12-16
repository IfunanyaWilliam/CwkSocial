using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.UserProfiles.Queries;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public GetUserProfileByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();
           
             var  profile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);

            if (profile is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));
                return result;
            }

            result.PayLoad = profile;
            return result;
        }
    }
}

