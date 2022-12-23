using System;
using AutoMapper;
using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Api.Contracts.Posts.Responses;

namespace CwkSocial.Api.MappingProfiles
{
    public class PostMappings : Profile
    {
        public PostMappings()
        {
            CreateMap<Post, PostResponse>();
            CreateMap<PostComment, PostCommentResponse>();
            CreateMap<PostInteraction, PostInteractionResponse>()
                 .ForMember(dest => dest.Type,
                            opt => opt.MapFrom(src => src.InteractionType.ToString()))
                 .ForMember(dest => dest.Author,
                            options => options.MapFrom(src => src.UserProfile));
        }
    }
}

