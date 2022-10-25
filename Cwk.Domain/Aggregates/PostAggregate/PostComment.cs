using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cwk.Domain.Exceptions;
using Cwk.Domain.Validators.PostValidators;

namespace Cwk.Domain.Aggregates.PostAggregate
{
    public class PostComment
    {
        private PostComment()
        {
        }

        public Guid CommentId { get; private set; }
        public Guid PostId { get; private set; }    
        public string? Text { get; private set; }
        public Guid UserProfileId { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }


        /// <summary>
        /// Creates a post comment
        /// </summary>
        /// <param name="postId">The Id of the post to which the comment belongs</param>
        /// <param name="text">Text content of the comment</param>
        /// <param name="userProfile">The Id of the user who created the comment</param>
        /// <returns><see cref="PostComment"/></returns>
        /// <exception cref="PostCommentNotValidException">Thrown if data provided for the post
        /// comment is not valid</exception>
        public static PostComment CreatePostComment(Guid postId, string text, Guid userProfile)
        {
            var validator = new PostCommentValidator();

            var objectToValidate = new PostComment
            {
                PostId          = postId,
                Text            = text,
                UserProfileId   = userProfile,
                DateCreated     = DateTime.UtcNow,
                LastModified    = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid)
                return objectToValidate;

            var exception = new PostCommentNotValidException("Post comment is not valid");

            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));

            throw exception;
        }

       

        public void UpdateCommentText(string newText)
        {
            Text         = newText;
            LastModified = DateTime.UtcNow;
        }

    }
}
