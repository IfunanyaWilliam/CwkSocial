using System;
using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Models;
using MediatR;

namespace CwkSocial.Application.Posts.Commands
{
    public class DeletePostCommand : IRequest<OperationResult<Post>>
    {
        public Guid PostId { get; set; }   
    }
}

