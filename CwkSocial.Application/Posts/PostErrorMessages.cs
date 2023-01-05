using System;
namespace CwkSocial.Application.Posts
{
    public class PostErrorMessages
    {
        public const string PostNotFound                    = "No post found with ID {0}";
        public const string PostDeleteNotPossible           = "Only the owner of a post can delete it";
        public const string PostUpdateNotPossible           = "Only the owner of a post can update it";
        public const string PostInteractionNotFound         = "Interaction not found";
        public const string InteractionRemovalNotAuthorized = "Cannot remove interaction as you are not its author";
    }
}


