using System;
namespace CwkSocial.Application.Enums
{
    public enum ErrorCode
    {
        NotFound                    = 404,
        ServerError                 = 500,

        //Validation errors
        ValidationError             = 101,

        //Infrastructure errors
        IdentityCreationFailed      = 202,
        


        //Application Error
        PostUpdateNotPossible           = 300,
        PostDeletNotPossible            = 301,
        InteractionRemovalNotAuthorized = 302,
        IdentityUserAlreadyExists       = 303,
        IdentityUserDoesNotExist        = 304,
        IncorrectPassword               = 305,
        UnAuthorizedAccountRemoval      = 306,


        UnknownError                = 999
    }
}

