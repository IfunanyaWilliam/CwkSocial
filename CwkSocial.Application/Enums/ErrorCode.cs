﻿using System;
namespace CwkSocial.Application.Enums
{
    public enum ErrorCode
    {
        NotFound                    = 404,
        ServerError                 = 500,

        //Validation errors
        ValidationError             = 101,

        //Infrastructure errors
        IdentityUserAlreadyExists   = 201,
        IdentityCreationFailed      = 202,
        IdentityUserDoesNotExist    = 203,
        IncorrectPassword           = 204,
        InexistentUserProfile       = 205,

        UnknownError                = 999
    }
}

