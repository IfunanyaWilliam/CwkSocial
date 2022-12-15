using System;
namespace CwkSocial.Application.Posts
{
    public class PostErrorMessages
    {
        public const string PostNotFound = "No post found with ID {0}";

        public const string PostDeleteNotPossible = "Only the owner of a post can delete it";
    }
}

