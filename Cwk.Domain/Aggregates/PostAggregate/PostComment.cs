using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public static PostComment CreatePostComment(Guid postId, string text, Guid userProfile)
        {
            return new PostComment
            {
                PostId          = postId,
                Text            = text,
                UserProfileId   = userProfile,
                DateCreated     = DateTime.UtcNow,
                LastModified    = DateTime.UtcNow
            };
        }

        public void UpdateCommentText(string newText)
        {
            Text         = newText;
            LastModified = DateTime.UtcNow;
        }

    }
}
