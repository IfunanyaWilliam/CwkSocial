using System;
using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Models;
using MediatR;

namespace CwkSocial.Application.Posts.Commands
{
    public class UpdatePostTextCommand : IRequest<OperationResult<Post>>
    {
        public string? NewText { get; set; }

        public Guid PostId { get; set; }
    }
}

