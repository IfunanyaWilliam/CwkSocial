using AutoMapper;
using CwkSocial.Api.Contracts.Posts.Requests;
using CwkSocial.Api.Contracts.Posts.Responses;
using CwkSocial.Api.Filters;
using CwkSocial.Application.Posts.Commands;
using CwkSocial.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CwkSocial.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class PostsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostsController(IMediator mediator, IMapper mapper)
        {
            _mediator   = mediator;
            _mapper     = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _mediator.Send(new GetAllPosts());
            var mapped = _mapper.Map<List<PostResponse>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }


        [HttpGet]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id)
        {
            var postId  = Guid.Parse(id);
            var query   = new GetPostById() { PostId = postId };
            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<PostResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreatePost([FromBody] PostCreate newPost)
        {
            var command = new CreatePostCommand()
            {
                UserProfileId = newPost.UserProfileId,
                TextContent   = newPost.TextContent
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<PostResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors)
                    : CreatedAtAction(nameof(GetById), new { id = result.PayLoad.UserProfileId, mapped });
        }


        [HttpPatch]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePostText([FromBody] PostUpdate updatePost, string id)
        {
            var command = new UpdatePostTextCommand()
            {
                NewText = updatePost.Text,
                PostId  = Guid.Parse(id)
            };

            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }


        [HttpDelete]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var command = new DeletePostCommand() { PostId = Guid.Parse(id) };
            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NotFound();
        }

    }
}
