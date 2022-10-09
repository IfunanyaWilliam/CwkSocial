using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Application.Models;
using CwkSocial.Application.UserProfiles.Queries;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfiles, OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _ctx;

        public GetAllUserProfilesQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        
        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllUserProfiles request,
                                                         CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<UserProfile>>();
            result.PayLoad = await _ctx.UserProfiles.ToListAsync();
            return result;
        }
    }
}

