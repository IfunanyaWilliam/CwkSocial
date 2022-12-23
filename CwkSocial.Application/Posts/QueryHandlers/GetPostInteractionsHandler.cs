using System;
using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Queries;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.Posts.QueryHandlers
{
    public class GetPostInteractionsHandler : IRequestHandler<GetPostInteractions,OperationResult<List<PostInteraction>>>
    {
        private readonly DataContext _ctx;

        public GetPostInteractionsHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<PostInteraction>>> Handle(GetPostInteractions request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<PostInteraction>>();

            try
            {
                var post = await _ctx.Posts
                                     .Include(p => p.Interactions)
                                     .ThenInclude(u => u.UserProfile)
                                     .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);


                if (post == null)
                {
                    result.AddError(ErrorCode.NotFound, PostErrorMessages.PostNotFound);
                    return result;
                }

                result.PayLoad = post.Interactions.ToList();
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}

