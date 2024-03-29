﻿using System;
using CwkSocial.Application.UserProfiles.Queries;
using MediatR;

namespace CwkSocial.Api.Registrars
{
    public class BogardRegistrar : IWebApplicationBuilderRegistrar
    {
        public BogardRegistrar()
        {
        }

        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program), typeof(GetAllUserProfiles));
            builder.Services.AddMediatR(typeof(GetAllUserProfiles));
        }
    }
}

