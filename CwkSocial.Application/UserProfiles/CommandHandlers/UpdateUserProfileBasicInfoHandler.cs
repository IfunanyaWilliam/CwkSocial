using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using Cwk.Domain.Exceptions;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public UpdateUserProfileBasicInfoHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            try
            {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

                if (userProfile is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));

                    return result;
                }

                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress, request.Phone,
                                                          request.DateOfBirth, request.CurrentCity);
                userProfile.UpdateBasicInfo(basicInfo);

                _ctx.UserProfiles.Update(userProfile);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = userProfile;
                return result;
            }

            catch (UserProfileNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => result.AddError(ErrorCode.ValidationError, e));
            }

            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}

