using System;
using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Commands;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.Posts.CommandHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, OperationResult<Post>>
    {
        private readonly DataContext _ctx;

        public DeletePostHandler(DataContext ctx)
        {
            _ctx = ctx;
        }


        public async Task<OperationResult<Post>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);

                if (post is null)
                {
                    result.AddError(ErrorCode.NotFound,
                                    string.Format(PostErrorMessages.PostNotFound, request.PostId));
                    return result;
                }

                //Check that the UserProfileId of the user who wants to delete post matches the userProfileId of post owner
                if(post.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.PostDeletNotPossible, PostErrorMessages.PostDeleteNotPossible);
                    return result;
                }

                _ctx.Posts.Remove(post);
                await _ctx.SaveChangesAsync();

                result.PayLoad = post;
            }

            catch(Exception e)
            {
                result.AddError(ErrorCode.UnknownError, e.Message);
            }

            return result;
        }
    }
}

