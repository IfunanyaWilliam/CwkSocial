

using CwkSocial.Application.Enums;
using CwkSocial.Application.Identity.Commands;
using CwkSocial.Application.Models;
using CwkSocial.Application.UserProfiles;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.Identity.Handlers
{
    internal class RemoveAccountHandler : IRequestHandler<RemoveAccount, OperationResult<bool>>
    {
        private readonly DataContext _ctx;

        public RemoveAccountHandler(DataContext ctx)
        {
            _ctx= ctx;
        }
        public async Task<OperationResult<bool>> Handle(RemoveAccount request, 
                            CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                var identityUser = await _ctx.Users.FirstOrDefaultAsync(iu 
                        => iu.Id == request.IdentityUserId.ToString(), cancellationToken);

                if (identityUser == null)
                {
                    result.AddError(ErrorCode.IdentityUserDoesNotExist, 
                        IdentityErrorMessage.NonExistentIdentityUser);
                    return result;
                }

                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up
                    => up.IdentityId == request.IdentityUserId.ToString(), cancellationToken);

                if (userProfile == null)
                {
                    result.AddError(ErrorCode.NotFound, UserProfileErrorMessages.UserProfileNotFound);
                    return result;
                }

                if(identityUser.Id != request.RequesterGuid.ToString())
                {
                    result.AddError(ErrorCode.UnAuthorizedAccountRemoval, 
                            IdentityErrorMessage.UnAuthorizedAccountRemoval);
                    return result;
                }

                _ctx.UserProfiles.Remove(userProfile);
                _ctx.Users.Remove(identityUser);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = true;
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
