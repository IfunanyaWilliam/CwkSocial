using Cwk.Domain.Aggregates.UserProfileAggregate;
using Cwk.Domain.Exceptions;
using Cwk.Domain.Validators.PostValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwk.Domain.Aggregates.PostAggregate
{
    public class Post
    {
        private readonly List<PostComment> _comments = new List<PostComment>();
        private readonly List<PostInteraction> _interactions = new List<PostInteraction>();

        private Post()
        {
        }

        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile? UserProfile { get; private set; }
        public string? TextContent { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }
        public IEnumerable<PostComment>? Comments {  get { return _comments; }  }
        public IEnumerable<PostInteraction>? Interactions {  get { return _interactions; } }

        public static Post CreatePost(Guid userProfileId, string textContent)
        {
            var validator = new PostValidator();
            var objectToValidate = new Post
            {
                UserProfileId   = userProfileId,
                TextContent     = textContent,
                CreatedDate     = DateTime.Now,
                LastModified    = DateTime.Now
            };

            var validatorResult = validator.Validate(objectToValidate);
            if (validatorResult.IsValid)
                return objectToValidate;

            var exception = new PostNotValidException("Post is not valid");

            validatorResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));

            throw exception;
        }


        /// <summary>
        /// Updates the post text
        /// </summary>
        /// <param name="newText">The Updated post text</param>
        public void UpdatePostText(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
            {
                var exception = new PostNotValidException("Cannot update post. The text is invalid");

                exception.ValidationErrors.Add("The provided text is either null or white space");
                throw exception;
            }

            TextContent   = newText;
            LastModified  = DateTime.Now;
        }

        public void AddPostComment(PostComment newComment)
        {
            _comments.Add(newComment);
        }

        public void RemoveComment(PostComment toRemove)
        {
            _comments.Remove(toRemove);
        }

        public void AddInteraction(PostInteraction newInteraction)
        {
            _interactions.Add(newInteraction);
        }

        public void RemoveInteraction(PostInteraction toRemove)
        {
            _interactions.Remove(toRemove);
        }
    }
}
