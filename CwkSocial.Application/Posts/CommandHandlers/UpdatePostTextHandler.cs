﻿using System;
using Cwk.Domain.Aggregates.PostAggregate;
using Cwk.Domain.Exceptions;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Commands;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.Posts.CommandHandlers
{
    public class UpdatePostTextHandler : IRequestHandler<UpdatePostTextCommand, OperationResult<Post>>
    {
        private readonly DataContext _ctx;

        public UpdatePostTextHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Post>> Handle(UpdatePostTextCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId);

                if (post is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No Post with ID: {request.PostId} found"
                    };
                    result.Errors.Add(error);

                    return result;
                }

                if(post.UserProfileId != request.UserProfileId)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.PostUpdateNotPossible,
                        Message = $"Post update not possible. Only the owner can edit post."
                    };
                    result.Errors.Add(error);

                    return result;
                }

                post.UpdatePostText(request.NewText);

                await _ctx.SaveChangesAsync();
                result.PayLoad = post;

                return result;
            }
            catch (PostNotValidException e)
            {
                result.IsError = true;
                e.ValidationErrors.ForEach(er =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{e.Message}"
                    };

                    result.Errors.Add(error);
                });
            }

            catch (Exception e)
            {
                var error = new Error
                {
                    Code = ErrorCode.UnknownError,
                    Message = $"{e.Message}"
                };

                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}

