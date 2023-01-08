using System;
namespace CwkSocial.Application.Identity
{
    public class IdentityErrorMessage
    {
        public const string NonExistentIdentityUser = "Unable to find a user with the specified username";
        public const string IncorrectPassword = "The password provided is incorrect";
        public const string IdentityUserAlreadyExists = "Provided email already exists. Cannot register user";
        public const string UnAuthorizedAccountRemoval = "Cannot remove account as you are not its owner";
    }
}

