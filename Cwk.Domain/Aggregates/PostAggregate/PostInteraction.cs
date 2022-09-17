using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwk.Domain.Aggregates.PostAggregate
{
    public class PostInteraction
    {
        private PostInteraction()
        {
        }

        public Guid InteractionId { get; private set; }
        public Guid PostId { get; private set; }
        public InteractionType InteractionType { get; private set; }    

        public static PostInteraction CreatePostInteraction(Guid postId, InteractionType type)
        {
            return new PostInteraction
            {
                InteractionId = postId,
                InteractionType = type
            };
        }

    }
}
