using System;
using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Queries;
using CwkSocial.Dal;
using MediatR;

namespace CwkSocial.Application.Posts.QueryHandlers
{
    public class GetPostInteractionsHandler : IRequestHandler<GetPostInteractions,OperationResult<List<PostInteraction>>>
    {
        private readonly DataContext _ctx;

        public GetPostInteractionsHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public Task<OperationResult<List<PostInteraction>>> Handle(GetPostInteractions request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

