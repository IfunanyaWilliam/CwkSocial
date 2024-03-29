﻿using System;
using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Queries;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.Posts.QueryHandlers
{
    public class GetPostCommentHandler : IRequestHandler<GetPostComments, OperationResult<List<PostComment>>>
    {
        private readonly DataContext _ctx;

        public GetPostCommentHandler(DataContext ctx)
        {
            _ctx = ctx;
        }


        public async Task<OperationResult<List<PostComment>>> Handle(GetPostComments request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<PostComment>>();

            try
            {
                var posts = await _ctx.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId);

                result.PayLoad = posts.Comments.ToList();
            }
            catch(Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }

    }
}

