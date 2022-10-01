using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Application.UserProfiles.Queries;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById, UserProfile>
    {
        private readonly DataContext _ctx;

        public GetUserProfileByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<UserProfile> Handle(GetUserProfileById request, CancellationToken cancellationToken)
        {
            return await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);
        }
    }
}

