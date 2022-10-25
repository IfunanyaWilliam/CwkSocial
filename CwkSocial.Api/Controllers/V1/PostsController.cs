using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CwkSocial.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class PostsController : Controller
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
            return Ok();
        }


        [HttpGet]
        [Route(ApiRoutes.Posts.IdRoute)]
        public IActionResult GetById(int id)
        {
            return Ok("Hello! From V1");
        }
    }
}
