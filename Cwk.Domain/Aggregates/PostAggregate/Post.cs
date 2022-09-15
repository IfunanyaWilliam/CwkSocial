using Cwk.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwk.Domain.Aggregates.PostAggregate
{
    public class Post
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }
        public string? TextContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public ICollection<PostComment>? Posts { get; set; }
        public ICollection<PostInteraction>? Interactions { get; set; }
    }
}
