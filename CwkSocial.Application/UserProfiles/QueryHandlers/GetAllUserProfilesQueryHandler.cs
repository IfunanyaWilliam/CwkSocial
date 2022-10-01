using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Application.UserProfiles.Queries;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfiles, IEnumerable<UserProfile>>
    {
        private readonly DataContext _ctx;

        public GetAllUserProfilesQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        
        public async Task<IEnumerable<UserProfile>> Handle(GetAllUserProfiles request,
                                                         CancellationToken cancellationToken)
        {
            return await _ctx.UserProfiles.ToListAsync();
        }
    }
}

