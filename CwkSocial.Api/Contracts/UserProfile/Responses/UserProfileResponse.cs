﻿using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;

namespace CwkSocial.Api.Contracts.UserProfile.Responses
{
    public record UserProfileResponse
    {
        public Guid UserProfileId { get; set; }
        public BasicInformation? BasicInfo { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}

