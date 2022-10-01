using System;
using AutoMapper;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.Dal;
using MediatR;

namespace CwkSocial.Application.UserProfiles.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserProfile>
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(DataContext ctx,
                                        IMapper mapper)
        {
            _ctx    = ctx;
            _mapper = mapper;
        }

        public async Task<UserProfile> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress, request.Phone,
                                                      request.DateOfBirth, request.CurrentCity);

            var userProfile = UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);

            _ctx.UserProfiles.Add(userProfile);
            await _ctx.SaveChangesAsync();

            return userProfile;
        }
    }

}

