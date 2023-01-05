using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Commands;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.Posts.CommandHandlers
{
    public class RemovePostInteractionHandler : IRequestHandler<RemovePostInteraction, OperationResult<PostInteraction>>
    {
        private readonly DataContext _ctx;

        public RemovePostInteractionHandler(DataContext ctx)
        {
            _ctx = ctx;       
        }

        public async Task<OperationResult<PostInteraction>> Handle(RemovePostInteraction request, 
                            CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostInteraction>();
            try
            {
                var post = await _ctx.Posts.Include(p => p.Interactions)
                                            .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);
                if (post == null)
                {
                    result.AddError(ErrorCode.NotFound,
                                    string.Format(PostErrorMessages.PostNotFound, request.PostId));
                    return result;
                }

                var interaction = post.Interactions.FirstOrDefault(i => i.InteractionId== request.PostId);

                if(interaction== null)
                {
                    result.AddError(ErrorCode.NotFound, PostErrorMessages.PostInteractionNotFound);
                    return result;
                }

                if(interaction.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.InteractionRemovalNotAuthorized, PostErrorMessages.InteractionRemovalNotAuthorized);
                    return result;
                }

                post.RemoveInteraction(interaction);
                _ctx.Posts.Update(post);
                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = interaction;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
