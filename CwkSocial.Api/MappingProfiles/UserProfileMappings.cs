using System;
using AutoMapper;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using CwkSocial.Api.Contracts.UserProfile.Requests;
using CwkSocial.Api.Contracts.UserProfile.Responses;
using CwkSocial.Application.UserProfiles.Commands;

namespace CwkSocial.Api.MappingProfiles
{
    public class UserProfileMappings : Profile
    {
        public UserProfileMappings()
        {
            CreateMap<UserProfileCreateUpdate, UpdateUserProfileBasicInfo>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();

        }
    }
}

