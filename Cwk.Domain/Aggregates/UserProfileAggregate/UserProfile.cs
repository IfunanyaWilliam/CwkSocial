﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwk.Domain.Aggregates.UserProfileAggregate
{
    public class UserProfile
    {
        private UserProfile()
        {
        }

        public Guid UserProfileId { get; private set; }
        public string? IdentityId { get; private set; }
        public BasicInfo? BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }


        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            //To Do: Add validation, error handling strategies, error notification 

            return new UserProfile
            {
                IdentityId   = identityId,
                BasicInfo    = basicInfo,
                DateCreated  = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo    = newInfo;
            LastModified = DateTime.UtcNow;
        }
    }
}
