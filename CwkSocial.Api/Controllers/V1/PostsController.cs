using System.Security.Claims;
using AutoMapper;
using CwkSocial.Api.Contracts.Common;
using CwkSocial.Api.Contracts.Posts.Requests;
using CwkSocial.Api.Contracts.Posts.Responses;
using CwkSocial.Api.Extensions;
using CwkSocial.Api.Filters;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Commands;
using CwkSocial.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CwkSocial.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new CreatePostCommand()
            {
                UserProfileId = userProfileId,
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
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new UpdatePostTextCommand()
            {
                NewText         = updatePost.Text,
                PostId          = Guid.Parse(id),
                UserProfileId   = userProfileId
            };

            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }


        [HttpDelete]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new DeletePostCommand()
            {
                PostId          = Guid.Parse(id),
                UserProfileId   = userProfileId
            };
            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }


        [HttpGet]
        [Route(ApiRoutes.Posts.PostComments)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetCommentsByPostId(string postId)
        {
            var query = new GetPostComments() { PostId = Guid.Parse(postId) };
            var result = await _mediator.Send(query);

            if(result.IsError)
                HandleErrorResponse(result.Errors);

            var comments = _mapper.Map<List<PostCommentResponse>>(result.PayLoad);

            return Ok(comments); 
        }


        [HttpPatch]
        [Route(ApiRoutes.Posts.PostComments)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> AddCommentToPost(string postId, [FromBody] PostCommentCreate comment)
        {
            var isValidGuid = Guid.TryParse(comment.UserProfileId, out var userProfileId);

            if(!isValidGuid)
            {
                var apiError = new ErrorResponse();

                apiError.StatusCode = 404;
                apiError.StatusPhrase = "Bad Request";
                apiError.TimeStamp = DateTime.Now;
                apiError.Errors.Add("User profile ID is not in a Valid Guid Format");

                return BadRequest(apiError);
            }

            var command = new AddPostComment()
            {
                PostId          = Guid.Parse(postId),
                UserProfileId   = userProfileId,
                CommentText     = comment.Text
            };

            var result = await _mediator.Send(command);

            if (result.IsError)
                return HandleErrorResponse(result.Errors);

            var newComment = _mapper.Map<PostCommentResponse>(result.PayLoad);

            return Ok(newComment);
        }


        [HttpGet]
        [Route(ApiRoutes.Posts.PostInteractions)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetPostInteractions(string postId, CancellationToken token)
        {
            var postGuid = Guid.Parse(postId);
            var query = new GetPostInteractions() { PostId = postGuid };
            var result = await _mediator.Send(query, token);

            if (result.IsError) HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<List<PostInteractionResponse>>(result.PayLoad);
            return Ok(mapped);
        }
    }
}
