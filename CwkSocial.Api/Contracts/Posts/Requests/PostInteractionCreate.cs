using System;
using System.ComponentModel.DataAnnotations;
using Cwk.Domain.Aggregates.PostAggregate;

namespace CwkSocial.Api.Contracts.Posts.Requests
{
    public class PostInteractionCreate
    {
        [Required]
        public InteractionType Type { get; set; }
    }
}

