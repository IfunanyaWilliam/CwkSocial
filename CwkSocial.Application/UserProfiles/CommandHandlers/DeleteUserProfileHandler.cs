using System;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfile>
    {

        private readonly DataContext _ctx;

        public DeleteUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;
        }


        public async Task<Unit> Handle(DeleteUserProfile request, CancellationToken cancellationToken)
        {
            var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            _ctx.UserProfiles.Remove(userProfile);
            await _ctx.SaveChangesAsync();

            return new Unit();
        }
    }
}

